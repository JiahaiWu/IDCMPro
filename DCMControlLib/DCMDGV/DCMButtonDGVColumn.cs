using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCM.DCMDGV
{
    public class DCMButtonDGVColumn : DataGridViewColumn
    {
        public DCMButtonDGVColumn()
            : base(new DCMDGVButtonCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell. 
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DCMDGVButtonCell)))
                {
                    throw new InvalidCastException("Must be a DCMDGVButtonCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
