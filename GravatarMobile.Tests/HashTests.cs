using System.Text;
using GravatarMobile;

namespace GravatarMobile.Tests
{
    public class HashTests
    {
        // RFC 1321 test vectors
        [Theory]
        [InlineData("", "D41D8CD98F00B204E9800998ECF8427E")]
        [InlineData("a", "0CC175B9C0F1B6A831C399E269772661")]
        [InlineData("abc", "900150983CD24FB0D6963F7D28E17F72")]
        [InlineData("message digest", "F96B697D7CB7938D525A2F31AAF161D0")]
        public void Md5MatchesRfc1321(string input, string expected)
        {
            Assert.Equal(expected, GravatarMobile.MD5.GetHashString(input));
            Assert.Equal(expected, GravatarMobile.MD5.GetHashString(Encoding.UTF8.GetBytes(input)));
            Assert.Equal(16, GravatarMobile.MD5.GetHash(input).Length);
        }

        [Fact]
        public void Md5RejectsNull()
        {
            Assert.Throws<ArgumentNullException>(() => GravatarMobile.MD5.GetHash((string)null));
            Assert.Throws<ArgumentNullException>(() => GravatarMobile.MD5.GetHash((byte[])null));
            Assert.Throws<ArgumentNullException>(() => GravatarMobile.MD5.GetHashString("a", null));
        }

        // Example from the Gravatar documentation
        [Theory]
        [InlineData("myemailaddress@example.com")]
        [InlineData("  MyEmailAddress@example.com ")]
        public void GravatarHashIsLowerCaseMd5OfTrimmedEmail(string email)
        {
            var gravatar = new Gravatar(email);

            Assert.Equal("0bc83cb571cd1c50ba6f3e8a78ef1346", gravatar.Hash);
        }

        [Fact]
        public void EmptyEmailIsRejected()
        {
            Assert.Throws<Exception>(() => new Gravatar(" "));
        }

        [Fact]
        public void ImageAndProfileThrowUntilLoaded()
        {
            var gravatar = new Gravatar("nobody@example.invalid");

            Assert.Throws<Exception>(() => gravatar.Image);
            Assert.Throws<Exception>(() => gravatar.Profile);
        }
    }
}
