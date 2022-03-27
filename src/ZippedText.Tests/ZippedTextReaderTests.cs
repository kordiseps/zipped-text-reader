using System;
using System.Collections.Generic;
using Xunit;
using ZippedText.Reader;

namespace ZippedText.Tests
{
    public class ZippedTextReaderTests
    {
        
        /////////////////////////////// SHOULD CREATE INSTANCE /////////////////////////////
        [Fact]
        public void Should_Create_Instance()
        {
            var zipContent = new byte[]
            {
                80, 75, 3, 4, 20, 0, 0, 0, 8, 0,
                64, 182, 118, 84, 101, 85, 229, 224, 15, 0,
                0, 0, 19, 0, 0, 0, 8, 0, 0, 0,
                116, 101, 120, 116, 46, 116, 120, 116, 203, 201,
                204, 75, 53, 228, 229, 202, 1, 82, 70, 16,
                202, 24, 0, 80, 75, 1, 2, 20, 0, 20,
                0, 0, 0, 8, 0, 64, 182, 118, 84, 101,
                85, 229, 224, 15, 0, 0, 0, 19, 0, 0,
                0, 8, 0, 0, 0, 0, 0, 0, 0, 1,
                0, 32, 0, 0, 0, 0, 0, 0, 0, 116,
                101, 120, 116, 46, 116, 120, 116, 80, 75, 5,
                6, 0, 0, 0, 0, 1, 0, 1, 0, 54,
                0, 0, 0, 53, 0, 0, 0, 0, 0
            };
            ZippedTextReader reader = new ZippedTextReader(zipContent);
            Assert.NotNull(reader);
            Assert.True(reader.Read());
            Assert.NotEmpty((List<string>)reader);
        }

        //////////////////// SHOULD THROW EXCEPTION ON CREATE INSTANCE /////////////////////
        [Theory]
        [MemberData(nameof(Get_Params_For_Should_Throw_Exception_On_Create_Instance))]
        public void Should_Throw_Exception_On_Create_Instance(byte[] zipContent)
        {
            void act() => new ZippedTextReader(zipContent);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.NotNull(exception.Message);
            Assert.Equal("Given argument is not valid compressed data", exception.Message);
        }

        public static IEnumerable<object[]> Get_Params_For_Should_Throw_Exception_On_Create_Instance()
        {
            yield return new object[] { new byte[] { 80, 75, 3, 2, 15 } };
            yield return new object[] { new byte[] { 0, 75, 3, 4, 20, 0, } };
        }


    }
}