namespace Ebleme.Utility {

    public class MoveableBillboard : Billboard {
        public bool UpdateEnabled { get; set; } = true;

        private void LateUpdate() {
            if (UpdateEnabled) {
                UpdateBillboard();
            }
        }

        public override void SetBillboardState(bool state) {
            base.SetBillboardState(state);
            UpdateEnabled = state;
        }
    }
}
