using MDA.Messaging;
using MDA.Messaging.Impl;
using Xunit;

namespace MDA.Tests.Messaging
{
    /// <summary>
    /// �ڴ涩�Ĺ����������ࡣ
    /// </summary>
    public class InMemory_Message_Subscriber_Manager_Test
    {
        private readonly IMessageSubscriberManager _subscriberManager = new InMemoryMessageSubscriberManager(new DefaultMessageSubscriberCollection());
        /// <summary>
        /// ��һ�δ�����������Ӧ��Ϊ�ա�
        /// </summary>
        [Fact]
        public void After_Creation_Should_Be_Empty()
        {
            var subscriberManager = new InMemoryMessageSubscriberManager(new DefaultMessageSubscriberCollection());

            Assert.True(_subscriberManager.IsEmpty);
        }

        /// <summary>
        /// ���һ�������ߺ�Ӧ�ð����ö����ߡ�
        /// </summary>
        [Fact]
        public void After_One_Subscriber_Should_Contain_The_Subscriber()
        {
            _subscriberManager.Subscribe<TestMessage, TestMessageHandler>();
            _subscriberManager.SubscribeDynamic<TestDynamicMessageHandler>("TestDynamicMessageHandler");

            Assert.True(_subscriberManager.HasSubscriber<TestMessage>());
            Assert.True(_subscriberManager.HasSubscriber("TestDynamicMessageHandler"));
        }
    }
}
