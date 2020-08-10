using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace ElementFiltering
{
    class ElementFiltering : IExternalCommand
    {
        //Filter System Family Types by type
        public void FilterSystemFamByType(Document m_rvtDoc)
        {
            var wallTypeCollector1 = new FilteredElementCollector(m_rvtDoc).WherePasses(new ElementClassFilter(typeof(WallType)));
            IList<Element> wallTypes1 = wallTypeCollector1.ToElements();
        }

        //Filter System Family Types by Class
        public void FilterSystemFamByClass(Document m_rvtDoc)
        {
            FilteredElementCollector wallTypeCollector3 = new FilteredElementCollector(m_rvtDoc).OfClass(typeof(WallType));
        }

        public void FilterLoadedFamByCat(Document m_rvtDoc)
        {
            var doorTypeCollector = new FilteredElementCollector(m_rvtDoc);
            doorTypeCollector.OfClass(typeof(FamilySymbol));
            doorTypeCollector.OfCategory(BuiltInCategory.OST_Doors);
            IList<Element> doorTypes = doorTypeCollector.ToElements();
        }



        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Application m_rvtApp = commandData.Application.Application;
            Document m_rvtDoc = commandData.Application.ActiveUIDocument.Document;
            return Result.Succeeded;
        }
    }
}
