using MDA.Messaging.Impl;
using Xunit;

namespace MDA.Tests.Messaging
{
    /// <summary>
    /// �ڴ涩�Ĺ����������ࡣ
    /// </summary>
    public class InMemory_Message_Subscriber_Manager_Test
    {
        /// <summary>
        /// ��һ�δ�����������Ӧ��Ϊ�ա�
        /// </summary>
        [Fact]
        public void After_Creation_Should_Be_Empty()
        {
            var manager = new InMemoryMessageSubscriberManager();

            Assert.True(manager.IsEmpty);
        }

        /// <summary>
        /// ���һ�������ߺ�Ӧ�ð����ö����ߡ�
        /// </summary>
        [Fact]
        public void After_One_Subscriber_Should_Contain_The_Subscriber()
        {
            var manager = new InMemoryMessageSubscriberManager();
            manager.Subscribe<TestMessage, TestMessageHandler>();
            manager.SubscribeDynamic<TestDynamicMessageHandler>("TestDynamicMessageHandler");

            Assert.True(manager.HasSubscriber<TestMessage>());
            Assert.True(manager.HasSubscriber("TestDynamicMessageHandler"));
        }
    }
}
