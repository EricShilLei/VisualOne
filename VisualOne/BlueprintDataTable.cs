using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedLib;

namespace VisualOne
{
    class BluePrintDataTable : DataTable
    {
        public BluePrintDataTable()
        {
            CreateColumns();
        }

        protected override Type GetRowType()
        {
            return typeof(BluePrintDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new BluePrintDataRow(builder);
        }

        public void AddBlueprints(List<BluePrint> blueprints)
        {
            this.Rows.Clear();

            foreach (var bp in blueprints)
            {
                BluePrintDataRow br = (BluePrintDataRow)NewRow();
                br.BindBlueprint(bp);
                this.Rows.Add(br);
            }
        }

        public BluePrintDataRow this[int idx]
        {
            get { return (BluePrintDataRow)Rows[idx]; }
        }

        private void CreateColumns()
        {
            Columns.Add(new DataColumn("Guid", typeof(string)));
            Columns.Add(new DataColumn("Theme", typeof(string)));
            Columns.Add(new DataColumn("Variant", typeof(string)));
            Columns.Add(new DataColumn("Layout", typeof(string)));
            Columns.Add(new DataColumn("Type", typeof(string)));
            Columns.Add(new DataColumn("Crop", typeof(string)));
            Columns.Add(new DataColumn("KeptRate", typeof(double)));
            Columns.Add(new DataColumn("Kept", typeof(uint)));
            Columns.Add(new DataColumn("Seen", typeof(uint)));
        }
    }

    public class BluePrintDataRow : DataRow
    {
        internal BluePrintDataRow(DataRowBuilder builder) : base(builder)
        {
        }

        public void BindBlueprint(BluePrint bp)
        {
            m_bp = bp;
            this["Guid"] = bp.Guid.ToString();
            this["Theme"] = bp.Theme;
            this["Variant"] = bp.Variant;
            this["Layout"] = bp.Layout;
            this["Type"] = bp.Type;
            this["Crop"] = bp.CropNonCrop;
            this["KeptRate"] = bp.KeptRate;
            this["Kept"] = bp.Kept;
            this["Seen"] = bp.Seen;
        }

        public BluePrint GetBlueprint()
        {
            return m_bp;
        }
        private BluePrint m_bp;
    }
}
