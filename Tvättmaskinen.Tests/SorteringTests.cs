using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NSubstitute;
using Xunit;

namespace Tvättmaskinen.Tests
{
    public class SorteringTests
    {
        [Fact]
        public void Sort_GivenAValidPath_ShouldCreateFolderWithNameTvättade()
        {
            var testFilesDirectory = GetTestFilesDirectory();
            var folderPath = testFilesDirectory + "\\Tvättade";
            var sut = CreateSut();
            var name = "TestPerson";


            sut.Sort(testFilesDirectory, name);

            Assert.True(Directory.Exists(folderPath));

        }
        [Fact]
        public void GetAllXmlFiles_ShouldGetAllXmlFilesWithinFolder()
        {
            var testFilesDirectory = GetTestFilesDirectory();
            var sut = CreateSut();

            var actual = sut.GetAllXmlFiles(testFilesDirectory);

            const int expectedNumberOfFiles = 5;
            Assert.Equal(expectedNumberOfFiles, actual.Length);
        }

        [Fact]
        public void GetSavePath_ShouldReturnString()
        {
            var testFilesDirectory = GetTestFilesDirectory();
            var sut = CreateSut();

            var actual = sut.SavePath(testFilesDirectory);

            var expected = testFilesDirectory + "/Tvättade/";
            Assert.Equal(expected, actual);
        }

        //[Fact]
        //public void CreateNewFolder_ShouldCreateANewFolder()
        //{
        //    var testFilesDirectory = GetTestFilesDirectory();
        //    var sut = CreateSut();

        //    var actual = sut.CreateNewFolder(testFilesDirectory);

        //    var expected = testFilesDirectory + "/Tvättade/";
        //    Assert.Equal(expected, actual);
        //}

        private Sortering CreateSut()
        {
            return new Sortering(Substitute.For<IMisLifepDoc>(), Substitute.For<IMisLife162>(), Substitute.For<IMisLife172>());
        }

        private string GetTestFilesDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var directoryPath = Path.GetDirectoryName(path);
            return Path.GetFullPath(Path.Combine(directoryPath, @"..\..\..\TestFiler"));
        }
    }
}
