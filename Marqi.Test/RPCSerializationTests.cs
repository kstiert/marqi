using Marqi.Common;
using Marqi.Common.Fonts;
using ServiceWire.NamedPipes;

namespace Marqi.Test
{
    public class Tests
    {
        private const string PIPE = "pipe";

        private NpClient<IThing> client;

        private NpHost host;

        private Thing thing;

        [SetUp]
        public void Setup()
        {
            thing = new Thing();
            host = new NpHost(PIPE);
            host.AddService<IThing>(thing);
            host.Open();

            client = new NpClient<IThing>(new NpEndPoint(PIPE));
        }

        [TearDown]
        public void Teardown()
        {
            client.Dispose();
            host.Close();
            host.Dispose();
        }

        [Test]
        public void ColorTest()
        {
            var c = new Color(201, 202, 203);

            client.Proxy.ColorThing(c);

            Assert.That(thing.Color.R, Is.EqualTo(c.R));
            Assert.That(thing.Color.G, Is.EqualTo(c.G));
            Assert.That(thing.Color.B, Is.EqualTo(c.B));
        }

        [Test]
        public void FontTest()
        {
            var f = new Font(1);

            client.Proxy.FontThing(f);

            Assert.That(thing.Font.Id, Is.EqualTo(f.Id));
        }
    }
}