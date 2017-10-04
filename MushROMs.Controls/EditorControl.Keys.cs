using System.Drawing;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    partial class EditorControl
    {
        #region Fields
        /// <summary>
        /// A value that determines whether selection keys are being held down.
        /// </summary>
        private bool hold;
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="Control.KeyDown"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="KeyEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            ProcessEditorKeyDown(e);
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Processes the <see cref="Control.KeyDown"/> logic for the
        /// <see cref="EditorControl"/>.
        /// </summary>
        /// <param name="e">
        /// A <see cref="KeyEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ProcessEditorKeyDown(KeyEventArgs e)
        {
            if (this.Editor == null)
                return;

            // Get the active point.
            Point p = this.Editor.Active.RelativePoint;

            // Scroll the point if possible.
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (p.X > 0 || this.Editor.Selecting)
                        p.X--;
                    break;
                case Keys.Right:
                    if (p.X < this.Editor.MapSize.Width - 1 || this.Editor.Selecting)
                        p.X++;
                    break;
                case Keys.Up:
                    if (p.Y > 0 || this.Editor.Selecting)
                        p.Y--;
                    break;
                case Keys.Down:
                    if (p.Y < this.Editor.MapSize.Height - 1 || this.Editor.Selecting)
                        p.Y++;
                    break;
            }

            // Set the active tile if we moved it.
            if (p != this.Editor.Active.RelativePoint)
            {
                // Finish the selection if the modifier key is released.
                if (!e.Shift)
                    this.Editor.FinalizeSelection();

                this.Editor.SetActiveTile(p);
            }

            // Initialize selection if modifier key was pressed.
            if (!hold && e.Shift && !e.Alt)
            {
                hold = true;
                this.Editor.InitializeSelection();
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.KeyUp"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="KeyEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            ProcessEditorKeyUp(e);
            base.OnKeyUp(e);
        }

        /// <summary>
        /// Processes the <see cref="Control.KeyDown"/> logic for the
        /// <see cref="EditorControl"/>.
        /// </summary>
        /// <param name="e">
        /// A <see cref="KeyEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ProcessEditorKeyUp(KeyEventArgs e)
        {
            if (this.Editor == null)
                return;

            // Finalize selection if modifier key was released.
            if (hold && !e.Shift && !e.Alt)
            {
                hold = false;
                this.Editor.FinalizeSelection();
            }
            else
                hold = e.Shift;
        }
        #endregion
    }
}