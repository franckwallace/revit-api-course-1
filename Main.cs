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

namespace LessonFile
//namespace RevitTest
{
    [Transaction(TransactionMode.Manual)]

    public class Main : IExternalCommand
    //public class Class1 : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //UIApplication uiapp = commandData.Application;
            //UIDocument uidoc = uiapp.ActiveUIDocument;
            //Application app = uiapp.Application;
            //Document doc = uidoc.Document;

            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var app = uiapp.Application;
            var doc = uidoc.Document;

            //var categories = new List<BuiltInCategory>()
            //{
            //    BuiltInCategory.OST_Walls,
            //    BuiltInCategory.OST_Floors,
            //};

            //var multiCategoryFilter = new ElementMulticategoryFilter(categories);

            //var collector = new FilteredElementCollector(doc)
            //    .WherePasses(multiCategoryFilter);

            ////var collector = new FilteredElementCollector(doc)
            ////    .OfCategory(BuiltInCategory.OST_Walls)
            ////    .WhereElementIsNotElementType();

            ////var collector = new FilteredElementCollector(doc)
            ////    .OfClass(typeof(Wall));
            ///

            var familyInstanceFilter = new ElementClassFilter(typeof(FamilyInstance));

            // Create a category filter for Doors
            var doorsCategoryfilter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);

            // Create a logic And filter for all Door FamilyInstances
            var logicalAndFilter = new LogicalOrFilter(familyInstanceFilter, doorsCategoryfilter);

            // Apply the filter to the elements in the active document
            var collector = new FilteredElementCollector(doc)
            .WherePasses(logicalAndFilter);


            var simpleForm = new SimpleForm(collector);
            simpleForm.ShowDialog();
            

            return Result.Succeeded;
        }
    }
}
