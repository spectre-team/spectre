using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Spectre.Dependencies
{
    public static class FileSystemDependency
    {
        private static IKernel _kernel;

        public static void Initialize(IFileSystem fileSystem)
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IFileSystem>()
                .ToConstant(fileSystem);
        }

        public static IKernel GetKernel() => _kernel;

        public static IFileSystem GetFileSystem() => _kernel.Get<IFileSystem>();
    }
}
