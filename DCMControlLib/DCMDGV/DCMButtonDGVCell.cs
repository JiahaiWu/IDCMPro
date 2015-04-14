using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCM.DCMDGV
{
    public class DCMDGVButtonCell:DataGridViewTextBoxCell
    {
        public DCMDGVButtonCell()
            : base()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value. 
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            DCMButtonEditControl ctl =
                DataGridView.EditingControl as DCMButtonEditControl;
            // Use the default row value when Value property is null. 
            if (this.Value == null)
            {
                ctl.Text = this.DefaultNewRowValue.ToString();
            }
            else
            {
                ctl.Text = this.Value.ToString();
            }
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses. 
                return typeof(DCMTextEditControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains. 
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return "";
            }
        }
    }
}
