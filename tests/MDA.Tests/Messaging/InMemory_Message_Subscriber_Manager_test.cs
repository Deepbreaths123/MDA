using MDA.Message;
using MDA.Message.Abstractions;
using System.Linq;
using Xunit;

namespace MDA.Tests.Messaging
{
    /// <summary>
    /// �ڴ涩�Ĺ����������ࡣ
    /// </summary>
    public class InMemory_Message_Subscriber_Manager_Test
    {
        private readonly IMessageSubscriberManager _subscriberManager = new InMemoryMessageSubscriberManager(new DefaultMessageSubscriberCollection());

        [Fact(DisplayName = "��һ�δ�����������Ӧ��Ϊ�ա�")]
        public void After_Creation_Should_Be_Empty()
        {
            var subscriberManager = new InMemoryMessageSubscriberManager(new DefaultMessageSubscriberCollection());

            Assert.True(subscriberManager.IsEmpty);
        }

        [Fact(DisplayName = "���һ�������ߺ�Ӧ�ð����ö����ߡ�")]
        public void After_One_Subscriber_was_Added_Should_Contain_The_Subscriber()
        {
            _subscriberManager.Subscribe<TestMessage, TestMessageHandler>();
            _subscriberManager.SubscribeDynamic<TestDynamicMessageHandler>("TestDynamicMessageHandler");

            Assert.True(_subscriberManager.HasSubscriber<TestMessage>());
            Assert.True(_subscriberManager.HasSubscriber("TestDynamicMessageHandler"));
        }

        [Fact(DisplayName = "���һ�������ߺ���Ϣ������Ӧ��Ψһ��")]
        public void After_One_Subcriber_Was_Added_The_Message_Handler_Should_Be_One()
        {
            _subscriberManager.Subscribe<TestMessage, TestMessageHandler>();
            _subscriberManager.SubscribeDynamic<TestDynamicMessageHandler>("TestDynamicMessageHandler");

            var subscriblers = _subscriberManager.GetSubscribers<TestMessage>();
            var dynamicSubscriblers = _subscriberManager.GetSubscribers("TestDynamicMessageHandler");

            Assert.Single(subscriblers);
            Assert.Single(dynamicSubscriblers);
        }

        [Fact(DisplayName = "�Ƴ����еĶ����ߺ󣬶������б�Ӧ��Ϊ�ա�")]
        public void After_All_Subcriber_Be_Removed_Subscriber_Should_Be_Empty()
        {
            _subscriberManager.Subscribe<TestMessage, TestMessageHandler>();
            _subscriberManager.Subscribe<TestMessage, TestMessageHandler1>();
            _subscriberManager.SubscribeDynamic<TestDynamicMessageHandler>("TestDynamicMessageHandler");
            _subscriberManager.SubscribeDynamic<TestDynamicMessageHandler1>("TestDynamicMessageHandler");

            _subscriberManager.Unsubscribe<TestMessage, TestMessageHandler>();
            _subscriberManager.Unsubscribe<TestMessage, TestMessageHandler1>();
            _subscriberManager.UnsubscribeDynamic<TestDynamicMessageHandler>("TestDynamicMessageHandler");
            _subscriberManager.UnsubscribeDynamic<TestDynamicMessageHandler1>("TestDynamicMessageHandler");

            Assert.True(_subscriberManager.IsEmpty);
        }

        [Fact(DisplayName = "��ȡ���ж����ߡ�")]
        public void Get_Handlers_For_Message_Should_Return_All_Handlers()
        {
            _subscriberManager.Subscribe<TestMessage, TestMessageHandler>();
            _subscriberManager.Subscribe<TestMessage, TestMessageHandler1>();

            var _subscribers = _subscriberManager.GetSubscribers<TestMessage>();
            Assert.Equal(2, _subscribers.Count());

            _subscriberManager.SubscribeDynamic<TestDynamicMessageHandler>("TestDynamicMessageHandler");
            _subscriberManager.SubscribeDynamic<TestDynamicMessageHandler1>("TestDynamicMessageHandler");

            var _dynamicSubscribers = _subscriberManager.GetSubscribers("TestDynamicMessageHandler");
            Assert.Equal(2, _dynamicSubscribers.Count());
        }
    }
}
