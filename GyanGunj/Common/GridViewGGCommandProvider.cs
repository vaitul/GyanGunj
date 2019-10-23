using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace GyanGunj.Common
{
    public class GridViewGGCommandProvider : DefaultKeyboardCommandProvider
    {
        private GridViewDataControl grid;
        private Key lastKey;
        public GridViewGGCommandProvider(GridViewDataControl dataControl) : base(dataControl)
        {
            this.grid = dataControl;
            this.grid.RowEditEnded += Grid_RowEditEnded;
            this.grid.PreviewKeyDown += Grid_PreviewKeyDown;
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            this.lastKey = e.Key;
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Delete)
            {
                grid.CommitEdit();
            }
        }

        private void Grid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            if (lastKey != Key.Enter || lastKey != Key.Return)
                return;
            var lastColumn = GridUtils.LastColumn((RadGridView)grid);
            if (grid.Items.IndexOf(e.Row.Item) == grid.Items.Count - 1 && grid.CurrentColumn == lastColumn)
            {
                grid.CurrentColumn = GridUtils.FirstColumn((RadGridView)grid);
                grid.BeginInsert();
                grid.Focus();
            }
            lastKey = Key.Clear;
        }

        public override IEnumerable<ICommand> ProvideCommandsForKey(Key key)
        {
            List<ICommand> commandsToExecute = base.ProvideCommandsForKey(key).ToList();
            commandsToExecute.Clear();

            if (key == Key.Enter || key == Key.Return)
            {
                grid.CommitEdit();
                if (!string.IsNullOrEmpty(grid.CurrentCell?.Value?.ToString()) || grid.CurrentColumn != GridUtils.FirstColumn((RadGridView)grid))
                {
                    commandsToExecute.Add(RadGridViewCommands.MoveNext);
                    commandsToExecute.Add(RadGridViewCommands.ExtendSelectionToCurrentUnit);
                    commandsToExecute.Add(RadGridViewCommands.BeginEdit);
                }
                else
                {
                    grid.MoveFocus(new TraversalRequest(System.Windows.Input.FocusNavigationDirection.Down));
                }
            }
            else if (key == Key.Up || key == Key.Down || key == Key.Left || key == Key.Right || key == Key.Delete)
            {
                if (key == Key.Up)
                    commandsToExecute.Add(RadGridViewCommands.MoveUp);
                else if (key == Key.Down)
                    commandsToExecute.Add(RadGridViewCommands.MoveDown);
                else if (key == Key.Left)
                    commandsToExecute.Add(RadGridViewCommands.MoveLeft);
                else if (key == Key.Right)
                    commandsToExecute.Add(RadGridViewCommands.MoveRight);
                else if (key == Key.Delete)
                {
                    commandsToExecute.Add(RadGridViewCommands.Delete);
                    commandsToExecute.Add(RadGridViewCommands.MoveNext);
                }

                commandsToExecute.Add(RadGridViewCommands.ExtendSelectionToCurrentUnit);
                //commandsToExecute.Add(RadGridViewCommands.BeginEdit);
            }
            else if (key == Key.Escape)
            {
                commandsToExecute.Add(RadGridViewCommands.CancelCellEdit);
            }

            return commandsToExecute;
        }
    }
}
