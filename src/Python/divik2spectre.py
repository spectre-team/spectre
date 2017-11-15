"""Convert MATLAB data file into Spectre txt format

divik2spectre.py
Converts MATLAB data file into Spectre txt format

Copyright 2017 Spectre Team

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

================================================================================

Expected content of input data file:

a. m x n matrix data - contains m observations (measurement spots) described by
   n features (mass channels or convolutions of subsequent components
b. m x 2 matrix xy - for m observations contains X coordinate in first column
   and Y coordinate in second column
c. m x 1 matrix region - describes distinct biological structures; contains all
   numbers from 1 to p, where p is the number of distinct biological structures
d. p x 1 cell array region_names - content of i-th cell contains biological
   interpretation of region with label i in vector region
e. 1 x n matrix mz - contains m/z properties of mass channels or component means

"""

import argparse as agp
import h5py
import numpy as np
import scipy.io as scio
from tqdm import tqdm
from typing import List


def parse_args():
    """Parses command line arguments, requiring source and destination paths"""
    parser = agp.ArgumentParser(description="Convert DiviK data source mat-file"
                                            " into Spectre's data format")
    parser.add_argument(dest="source", help="Input MATLAB file")
    parser.add_argument(dest="destination", help="Output txt file")
    args = parser.parse_args()
    if not args.destination.endswith('.txt'):
        args.destination = args.destination + '.txt'
    return args


CONST_Z_COORDINATE = 0


def _serialize_entry(spectrum, location, region):
    """Serializes single spectrum with metadata

    :param spectrum: single spectrum values
    :param location: 2D coordinates of the spectrum
    :param region: assignment of spectrum to a Region-of-Interest
    :return: text file entry
    """
    return '\n'.join([
        ' '.join(map(str, [*location, CONST_Z_COORDINATE, region])),
        ' '.join(map(str, spectrum))
    ])


class DivikDataSerializer(object):
    """Serializes DiviK data into Spectre txt format"""
    def __init__(self, data, xy, mz, region, region_names):
        self.data = data
        self.xy = xy
        self.mz = mz.reshape((-1,))
        self.region = region.reshape((-1))
        self.region_names = region_names
        self._serialized = None

    def __str__(self):
        if self._serialized is None:
            header = '\n'.join([
                'region names: ' + ', '.join(self.region_names),
                ' '.join(map(str, self.mz))
            ])
            self._serialized = '\n'.join([
                header,
                *[_serialize_entry(spectrum, location, region)
                  for spectrum, location, region
                  in zip(tqdm(self.data, desc='Spectrum'), self.xy, self.region)]
            ])
        return self._serialized


def _parse_hdf5_cell_array(object_reference, matfile) -> List[str]:
    """Parses MATLAB cell array of character arrays from hdf5 file

    :param object_reference: cell array in the file
    :param matfile: mat-file parsed
    :return: list of strings
    """
    data = []
    for column in object_reference:
        row_data = []
        for row_number in range(len(column)):
            row_data.append(''.join(map(chr, matfile[column[row_number]][:])))
        data.append(row_data)
    data = list(np.transpose(data)[0])
    return data


def read_hdf5(path: str) -> DivikDataSerializer:
    """Parses hdf5 mat-file with DiviK data into serializer"""
    matfile = h5py.File(path, 'r')
    region_names = _parse_hdf5_cell_array(matfile['region_names'], matfile)
    data = np.transpose(matfile['data'])
    xy = np.transpose(matfile['xy']).astype(int)
    assert data.shape[0] == xy.shape[0]
    assert xy.shape[1] == 2
    region = np.transpose(matfile['region']).astype(int)
    assert region.size == data.shape[0]
    if 'mz' not in matfile:
        print('M/Z array not found, creating fake...')
        mz = np.arange(data.shape[1])
    else:
        mz = np.transpose(matfile['mz'])
        assert mz.size == data.shape[1]
    return DivikDataSerializer(data, xy, mz, region, region_names)


def read_matfile(path: str) -> DivikDataSerializer:
    """Parses v7.0 >= mat-file with DiviK data into serializer"""
    matfile = scio.loadmat(path)
    region_names = [name_table[0] for name_table in matfile['region_names'][0]]
    data = matfile['data']
    xy = matfile['xy']
    assert data.shape[0] == xy.shape[0]
    assert xy.shape[1] == 2
    region = matfile['region']
    assert region.size == data.shape[0]
    if not 'mz' in matfile:
        print('M/Z array not found, creating fake...')
        mz = np.arange(data.shape[1])
    else:
        mz = matfile['mz']
        assert mz.size == data.shape[1]
    return DivikDataSerializer(data, xy, mz, region, region_names)


def main():
    args = parse_args()
    try:
        data = read_matfile(args.source)
    except NotImplementedError:
        data = read_hdf5(args.source)
    with open(args.destination, 'w') as output_file:
        output_file.write(str(data))


if __name__ == "__main__":
    main()
