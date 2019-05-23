using System.IO;
using NSubstitute;
using Xunit;

namespace Tvättmaskinen.Tests
{
    public class SorteringTests
    {
        [Fact]
        public void Sort_GivenAValidPath_ShouldCreateFolderWithNameTvättade()
        {
            const string path = @"C:\Users\Adam_\Desktop\MiP";
            const string folderPath = path + "\\Tvättade";
            var sut = CreateSortering();
            var name = "TestPerson";
            

            sut.Sort(path, name);

            Assert.True(Directory.Exists(folderPath));

        }
        [Fact]
        public void Sort_ShouldGroupFilesInDiffrentKeys()
        {
            const string path = @"C:\Users\Adam_\Desktop\MiP";
            var sut = CreateSortering();
            var name = "TestPerson";

            sut.Sort(path, name);

            Assert.True(result.Count == 4);
        }

        private Sortering CreateSortering()
        {
            return new Sortering(Substitute.For<IMisLifepDoc>(), Substitute.For<IMisLife16>(), Substitute.For<IMisLife17>());
        }
    }
}
