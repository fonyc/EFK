using EFK.Stats;

namespace EFK.Interactables
{
    public interface IInteractable
    {
        public void ShowInteraction(BaseStats baseStats);
        public void Interact(BaseStats baseStats);
        public void AddInteractableTag();
    }
}
