namespace Cosmic.Items.Food {
    using Cosmic.Entities;

    public abstract class FoodItem : Item {
        public override void Generate() {
            useHold = false;
            stack = 999;
        }

        public override void OnUse() {
            EntityManager.player.inventory.RemoveItem(this);
        }
    }
}