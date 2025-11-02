
namespace game.resource.settings.item
{
    public class MagicScriptBase
    {
        public string name;
        public int genre;
        public int detail;
        public int particular;
        public string image;
        public int objectId;
        public int width;
        public int height;
        public string intro;
        public int series;
        public int price;
        public int columnL;
        public int stackAllowed;
        public string script;
        public int skillId;
        public int columnP;
        public int columnQ;
        public int columnR;
        public int columnS;
        public int target;
        public int stackValue;
        public int columnV;
        public int columnW;
        public int columnX;
        public int columnY;
        public int columnZ;
        public int columnAA;
        public int columnAB;
        public int columnAC;
        public int columnAD;
        public int columnAE;

        ///////////////////////////////////////////////////////////////////////////

        public void Load(resource.Table table, int rowIndex)
        {
            this.name = table.Get<string>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.name, rowIndex);
            this.genre = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.genre, rowIndex);
            this.detail = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.detail, rowIndex);
            this.particular = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.particular, rowIndex);
            this.image = table.Get<string>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.image, rowIndex);
            this.objectId = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.objectId, rowIndex);
            this.width = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.width, rowIndex);
            this.height = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.height, rowIndex);
            this.intro = table.Get<string>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.intro, rowIndex);
            this.series = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.series, rowIndex);
            this.price = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.price, rowIndex);
            this.columnL = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnL, rowIndex);
            this.stackAllowed = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.stackAllowed, rowIndex);
            this.script = table.Get<string>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.script, rowIndex);
            this.skillId = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.skillId, rowIndex);
            this.columnP = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnP, rowIndex);
            this.columnQ = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnQ, rowIndex);
            this.columnR = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnR, rowIndex);
            this.columnS = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnS, rowIndex);
            this.target = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.target, rowIndex);
            this.stackValue = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.stackValue, rowIndex);
            this.columnV = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnV, rowIndex);
            this.columnW = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnW, rowIndex);
            this.columnX = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnX, rowIndex);
            this.columnY = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnY, rowIndex);
            this.columnZ = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnZ, rowIndex);
            this.columnAA = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnAA, rowIndex);
            this.columnAB = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnAB, rowIndex);
            this.columnAC = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnAC, rowIndex);
            this.columnAD = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnAD, rowIndex);
            this.columnAE = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicScript.columnAE, rowIndex);
        }

        public string GetKey()
        {
            return string.Empty + this.genre + ", " + this.detail + ", " + this.particular;
        }
    }
}
