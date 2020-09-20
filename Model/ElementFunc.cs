using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Windows.Forms;

#region This code is not used. Maybe it will be required for future development
/*

namespace HelloWorld.Model
{
    public static class ElementFunc
    {
        public static void FilterSystemFamByType(Document m_rvtDoc)
        {
            var wallTypeCollector1 = new FilteredElementCollector(m_rvtDoc).WherePasses(new ElementClassFilter(typeof(WallType)));
            IList<Element> wallTypes1 = wallTypeCollector1.ToElements();
        }

        //Filter System Family Types by Class
        public static void FilterSystemFamByClass(Document m_rvtDoc)
        {
            FilteredElementCollector wallTypeCollector3 = new FilteredElementCollector(m_rvtDoc).OfClass(typeof(WallType));
        }

        public static void FilterLoadedFamByCat(Document m_rvtDoc)
        {
            var doorTypeCollector = new FilteredElementCollector(m_rvtDoc);
            doorTypeCollector.OfClass(typeof(FamilySymbol));
            doorTypeCollector.OfCategory(BuiltInCategory.OST_Doors);
            IList<Element> doorTypes = doorTypeCollector.ToElements();
        }

        // Helper Function: parse the geometry element by geometry type. Here we look at the top level. 
        public static string GeometryElementToString(GeometryElement geomElem)
        {
            string str = string.Empty;
            foreach (GeometryObject geomObj in geomElem)
            {
                if (geomObj is Solid)
                {
                    // ex. wall 
                    Solid solid = (Solid)geomObj;
                    //str += GeometrySolidToString(solid) 
                    str += "Solid" + "\n";
                }
                else if (geomObj is GeometryInstance)
                {
                    // ex. door/window 
                    str += " -- Geometry.Instance -- " + "\n";
                    GeometryInstance geomInstance = (GeometryInstance)geomObj;
                    GeometryElement geoElem = geomInstance.SymbolGeometry;
                    str += GeometryElementToString(geoElem);
                }
                else if (geomObj is Curve)
                {
                    Curve curv = (Curve)geomObj;
                    //str += GeometryCurveToString(curv) 
                    str += "Curve" + "\n";
                }
                else if (geomObj is Mesh)
                {
                    Mesh mesh = (Mesh)geomObj;
                    //str += GeometryMeshToString(mesh) 
                    str += "Mesh" + "\n";
                }
                else
                {
                    str += " *** unkown geometry type" +
                         geomObj.GetType().Name;
                }
            }
            return str;
        }

        // Helper Function: returns XYZ in a string form. 
        public static string PointToString(XYZ pt)
        {
            if (pt == null)
            {
                return "";
            }
            return string.Format("({0},{1},{2})",
                pt.X.ToString("F2"), pt.Y.ToString("F2"), pt.Z.ToString("F2"));
        }

        //  example of retrieving a specific parameter indivisually. 
        //  (hard coding for simplicity. This function works best 
        //  with walls and doors.)
        public static void RetrieveParameter(Element elem, string header)
        {
            string s = string.Empty;
            // as an experiment, let's pick up some arbitrary parameters. 
            // comments - most of instance has this parameter 
            // (1) by BuiltInParameter. 
            Parameter param = elem.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
            if (param != null)
            {
                s += "Comments (by BuiltInParameter) = " + param.AsString() + "\n";
            }
            // (2) by name. (Mark - most of instance has this parameter.) 
            // if you use this method, it will language specific. 
            param = elem.LookupParameter("Mark");
            if (param != null)
            {
                s += "Mark (by Name) = " + param.AsString() + "\n";
            }
            // the following should be in most of type parameter 
            // 
            param = elem.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_COMMENTS);
            if (param != null)
            {
                s += "Type Comments (by BuiltInParameter) = " +
                param.AsString() + "\n";
            }
            param = elem.LookupParameter("Fire Rating");
            if (param != null)
            {
                s += "Fire Rating (by Name) = " + param.AsString() +
                    "\n";
            }
            // using the BuiltInParameter, you can sometimes access one that is
            // not in the parameters set. 
            // Note: this works only for element type. 
            param = elem.get_Parameter(BuiltInParameter.SYMBOL_FAMILY_AND_TYPE_NAMES_PARAM);
            if (param != null)
            {
                s += "SYMBOL_FAMILY_AND_TYPE_NAMES_PARAM (only by BuiltInParameter) = " +
                 param.AsString() + "\n";
            }
            param = elem.get_Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM);
            if (param != null)
            {
                s += "SYMBOL_FAMILY_NAME_PARAM (only by BuiltInParameter) = "
                    + param.AsString() + "\n";
            }
            // show it. 
            TaskDialog.Show(header, s);
        }

        public static void ShowBasicElementInfo(Document m_rvtDoc, Element elem)
        {
            // let's see what kind of element we got. 
            // 
            string s = "You Picked:" + "\n";

            s += " Class name = " + elem.GetType().Name + "\n";
            s += " Category = " + elem.Category.Name + "\n";
            s += " Element id = " + elem.Id.ToString() + "\n" + "\n";

            // and, check its type info. 
            // 
            //Dim elemType As ElementType = elem.ObjectType '' this is obsolete. 
            ElementId elemTypeId = elem.GetTypeId();
            ElementType elemType = (ElementType)m_rvtDoc.GetElement(elemTypeId);

            s += "Its ElementType:" + "\n";
            s += " Class name = " + elemType.GetType().Name + "\n";
            s += " Category = " + elemType.Category.Name + "\n";
            s += " Element type id = " + elemType.Id.ToString() + "\n";

            // finally show it. 

            TaskDialog.Show("Basic Element Info", s);
        }

        public static Element[] GetAllElemOfCategory(Document doc, BuiltInCategory category)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(category);
            Element[] elements = new Element[collector.ToElements().Count];
            int i = 0;
            foreach (Element el in collector.ToElements())
            {
                elements[i] = el;
                i++;
            }
            return elements;
        }

        public static void SetWorksetToElem(Document doc, string worksetName, Element[] elemArray)
        {
            if (elemArray == null) return;

            // Find all user worksets 
            FilteredWorksetCollector worksets = new FilteredWorksetCollector(doc).OfKind(WorksetKind.UserWorkset);

            Parameter[] wsparam = new Parameter[elemArray.Length];
            for (int i = 0; i < elemArray.Length; i++)
            {
                wsparam[i] = elemArray[i].get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);
            }
            if (wsparam == null) return;

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Change workset id");
                foreach (Workset ws in worksets)
                {
                    foreach (Parameter wp in wsparam)
                        if (ws.Name == worksetName) wp.Set(ws.Id.IntegerValue);
                }
                tx.Commit();
            }
        }

        public static Element PickElement(UIDocument rvtUIDoc)
        {
            Reference refPick = rvtUIDoc.Selection.PickObject(ObjectType.Element, "Pick an element");
            return rvtUIDoc.Document.GetElement(refPick);
        }

        public static void ShowParameters(Element elem, string header)
        {
            IList<Parameter> paramSet = elem.GetOrderedParameters();
            string s = string.Empty;
            foreach (Parameter p in paramSet)
            {
                string name = p.Definition.Name;
                string val = p.AsValueString();
                s += name + "=" + val + "\n";
            }
            TaskDialog.Show(header, s);
        }

        public static void PrintElemInfo(Element[] elem)
        {
            foreach (Element el in elem)
            {
                MessageBox.Show(el.Name);
                MessageBox.Show(el.GetType().ToString());
            }
        }

        public static void IdentifyElem(Element el)
        {
            string s = "";
            if (el is Wall) s = "Wall";
            else if (el is RoofBase) s = "Roof";
            else if (el.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Doors) s = "Doors";
            else if (el is HostObject) s = "System family instance";
            else s = "Other";
            TaskDialog.Show("Identify Element", s);
        }

        //Filter System Family Types by type
        public static void FilterSystemFamByType(Document m_rvtDoc)
        {
            var wallTypeCollector1 = new FilteredElementCollector(m_rvtDoc).WherePasses(new ElementClassFilter(typeof(WallType)));
            IList<Element> wallTypes1 = wallTypeCollector1.ToElements();
        }

        //Filter System Family Types by Class
        public static void FilterSystemFamByClass(Document m_rvtDoc)
        {
            FilteredElementCollector wallTypeCollector3 = new FilteredElementCollector(m_rvtDoc).OfClass(typeof(WallType));
        }

        public static void FilterLoadedFamByCat(Document m_rvtDoc)
        {
            var doorTypeCollector = new FilteredElementCollector(m_rvtDoc);
            doorTypeCollector.OfClass(typeof(FamilySymbol));
            doorTypeCollector.OfCategory(BuiltInCategory.OST_Doors);
            IList<Element> doorTypes = doorTypeCollector.ToElements();
        }

        // show the location information of the given element. location can be LocationPoint (e.g., furniture), and LocationCurve (e.g., wall).
        public static void ShowLocation(Element elem)
        {
            string s = "Location Information: " + "\n" + "\n";
            Location loc = elem.Location;
            if (loc is LocationPoint)
            {
                // (1) we have a location point 
                LocationPoint locPoint = (LocationPoint)loc;
                XYZ pt = locPoint.Point;
                double r = locPoint.Rotation;
                s += "LocationPoint" + "\n";
                s += "Point = " + PointToString(pt) + "\n";
                s += "Rotation = " + r.ToString() + "\n";
            }
            else if (loc is LocationCurve)
            {
                // (2) we have a location curve 
                LocationCurve locCurve = (LocationCurve)loc;
                Curve crv = locCurve.Curve;
                s += "LocationCurve" + "\n";
                s += "EndPoint(0)/Start Point = " +
                 PointToString(crv.GetEndPoint(0)) + "\n";
                s += "EndPoint(1)/End point = " +
                 PointToString(crv.GetEndPoint(1)) + "\n";
                s += "Length = " + crv.Length.ToString() + "\n";

                // Location Curve also has property JoinType at the end 
                s += "JoinType(0) = " + locCurve.get_JoinType(0).ToString() + "\n";
                s += "JoinType(1) = " + locCurve.get_JoinType(1).ToString() + "\n";
            }
            // show it. 
            TaskDialog.Show("Show Location", s);
        }

        // show the geometry information of the given element.
        public static void ShowGeometry(Autodesk.Revit.ApplicationServices.Application m_rvtApp, Element elem)
        {
            // Set a geometry option 
            Options opt = m_rvtApp.Create.NewGeometryOptions();
            opt.DetailLevel = ViewDetailLevel.Fine;

            // Get the geometry from the element 
            GeometryElement geomElem = elem.get_Geometry(opt);

            // If there is a geometry data, retrieve it as a string to show it.  
            string s = (geomElem == null) ?
              "no data" :
              GeometryElementToString(geomElem);

            TaskDialog.Show("Show Geometry", s);
        }
    }
}
*/
#endregion