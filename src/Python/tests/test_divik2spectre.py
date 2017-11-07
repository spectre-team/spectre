import unittest
import numpy as np
import divik2spectre as ds


class TestSerializeEntry(unittest.TestCase):
    def setUp(self):
        self.spectrum = np.array([1, 2, 3])
        self.location = np.array([4, 5])
        self.region = 6
        self.joined = ds._serialize_entry(self.spectrum, self.location,
            self.region)

    def test_joined_has_two_lines(self):
        self.assertEqual(len(self.joined.split('\n')), 2)

    def test_prints_metadata_in_first_line(self):
        first_line = self.joined.split('\n')[0]
        *coordinates, region = first_line.split()
        coordinates = list(map(int, coordinates))
        region = int(region)
        self.assertSequenceEqual(coordinates,
                                 [*self.location, ds.CONST_Z_COORDINATE])
        self.assertEqual(region, self.region)

    def test_prints_spectrum_in_second_line(self):
        second_line = self.joined.split('\n')[1]
        spectrum = list(map(float, second_line.split()))
        self.assertSequenceEqual(spectrum, list(self.spectrum))


class TestDivikDataSerializer(unittest.TestCase):
    def setUp(self):
        self.data = np.array([[11, 12, 3], [4, 5, 6]], dtype=np.float)
        self.xy = np.array([[15, 16], [17, 18]])
        self.mz = np.array([7, 8, 9], dtype=np.float)
        self.region = np.array([[1], [2]])
        self.region_names = ['first region name', 'second region name']

    def test_puts_region_names_in_global_metadata(self):
        serializer = ds.DivikDataSerializer(self.data, self.xy, self.mz, self.region, self.region_names)
        serialized = str(serializer)
        first_line = serialized.split('\n')[0]
        self.assertTrue(all([name in first_line for name in self.region_names]))

    def test_puts_mzs_after_global_metadata(self):
        serializer = ds.DivikDataSerializer(self.data, self.xy, self.mz, self.region, self.region_names)
        serialized = str(serializer)
        second_line = serialized.split('\n')[1]
        mzs = list(map(float, second_line.split()))
        self.assertSequenceEqual(mzs, list(self.mz))

    def test_puts_two_lines_per_each_spectrum(self):
        serializer = ds.DivikDataSerializer(self.data, self.xy, self.mz, self.region, self.region_names)
        serialized = str(serializer)
        number_of_lines = len(serialized.split('\n'))
        number_of_metadata_lines = 2
        self.assertEqual(number_of_lines - number_of_metadata_lines,
                         2 * self.data.shape[0])
