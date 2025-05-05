using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;



namespace CoreRevitModule

#region about transactions mode

//if transaction is set to read-only - the command cannot have any transactions
//and cannot call any methods that modify a document
//while this external command is being executed, nobody elsy is allowed to modify the document
//or have a transaction either
//To out it simply, this guarantees that during the entire execution of the external command
//the current models all remain unmodified

#endregion

{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            var selectedElement = uidoc.Selection.GetElementIds().Select(
                x => doc.GetElement(x)).First();

            var value = selectedElement.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString();
            TaskDialog.Show("Message", value);



            //var families = new List<Element>();
            //foreach (var familyId in familyIds)
            //{
            //    families.Add(
            //        doc.GetElement(familyId));
            //}


            return Result.Succeeded;
        }
    }
}
