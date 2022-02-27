using EFK.Stats;

namespace EFK.Interactables
{
    public interface IInteractable
    {
        public void Interact(CharactersStats playerAtributes);
        public void AddInteractableTag();
    }
}
