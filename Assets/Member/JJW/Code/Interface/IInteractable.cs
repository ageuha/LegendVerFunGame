using Code.Core.GlobalStructs;
using Code.Events;

namespace Member.JJW.Code.Interface
{
    public interface IInteractable
    {
        public void Interaction(InteractionContext context);
    }

    public struct InteractionContext
    {
        public EventChannel<Empty> EventChannel;

        public InteractionContext(EventChannel<Empty> eventChannel)
        {
            EventChannel = eventChannel;
        }
    }
}