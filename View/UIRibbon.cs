using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using ComboBox = Autodesk.Revit.UI.ComboBox;


namespace HelloWorld.View
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    // class for buttuns available during application mode
    public class ButtonAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication application, CategorySet b)
        {
            return true;
        }
    }

    public class UIRibbon : IExternalApplication
    {
        /// <summary>
        /// This is both the assembly name and the namespace 
        /// of the external command provider.
        /// </summary>
        const string IntroLabName = "HelloWorld";
        const string DllExtension = ".dll";
        /// <summary>
        /// Name of subdirectory containing images.
        /// </summary>
        const string ImageFolderName = "Images";
        /// <summary>
        /// Location of managed dll where we have defined the commands.
        /// </summary>
        string introLabPath;
        /// <summary>
        /// Location of images for icons.
        /// </summary>
        string imageFolder;


        /// <summary>
        /// Starting at the given directory, search upwards for 
        /// a subdirectory with the given target name located
        /// in some parent directory. 
        /// </summary>
        /// <param name="path">Starting directory, e.g. GetDirectoryName( GetExecutingAssembly().Location ).</param>
        /// <param name="target">Target subdirectory name, e.g. "Images".</param>
        /// <returns>The full path of the target directory if found, else null.</returns>

        string FindFolderInParents(string path, string target)
        {
            Debug.Assert(Directory.Exists(path), "expected an existing directory to start search in");
            string s;
            do
            {
                s = Path.Combine(path, target);
                if (Directory.Exists(s))
                {
                    return s;
                }
                path = Path.GetDirectoryName(path);
            } while (null != path);
            return null;
        }

        /// <summary>
        /// Load a new icon bitmap from our image folder.
        /// </summary>
        BitmapImage NewBitmapImage(string imageName)
        {
            return new BitmapImage(new Uri(Path.Combine(imageFolder, imageName)));
        }


        public void AddPushButtons(RibbonPanel panel)
        {
            // Set the information about the command we will be assigning to the button 
            PushButtonData pushButtonDataCreateFiles = new PushButtonData("CreateFiles", "Создать файлы", introLabPath, "CreatePrjFilesCom");
            // Add a button to the panel 
            PushButton pushButtonCreateFiles = panel.AddItem(pushButtonDataCreateFiles) as PushButton;
            // Add an icon 
            pushButtonCreateFiles.LargeImage = NewBitmapImage("ImgHelloWorld.png");
            // Add a tooltip for ex comment to command
            pushButtonCreateFiles.ToolTip = "Создание файлов модели по шаблону";
            //make button available for application mode 
            pushButtonCreateFiles.AvailabilityClassName = "ButtonAvailability";

            // Set the information about the command we will be assigning to the button 
            PushButtonData pushButtonDataInsertLink = new PushButtonData("InsertLink", "Вставить связь", introLabPath, "InsertRvtLinksCom");
            // Add a button to the panel 
            PushButton pushButtonInsertLink = panel.AddItem(pushButtonDataInsertLink) as PushButton;
            // Add an icon 
            pushButtonInsertLink.LargeImage = NewBitmapImage("ImgHelloWorld.png");
            // Add a tooltip for ex comment to command
            pushButtonInsertLink.ToolTip = "Вставка связи по общим координатам";
            //make button available for application mode 
            pushButtonInsertLink.AvailabilityClassName = "ButtonAvailability";

            /*
            // Set the information about the command we will be assigning to the button 
            PushButtonData pushButtonDataCreateWorksets = new PushButtonData("CreateWorksets", "Создать рабочие наборы", introLabPath, "CreateWorksetsCom");
            // Add a button to the panel 
            PushButton pushButtonCreateWorksets = panel.AddItem(pushButtonDataCreateWorksets) as PushButton;
            // Add an icon 
            pushButtonCreateWorksets.LargeImage = NewBitmapImage("ImgHelloWorld.png");
            // Add a tooltip for ex comment to command
            pushButtonCreateWorksets.ToolTip = "Создание рабочих наборов";
            //make button available for application mode 
            pushButtonCreateWorksets.AvailabilityClassName = "ButtonAvailability";
            */
        }


        public void AddSplitButton(RibbonPanel panel)
        {
            // Create three push buttons for split button drop down 

            // #1 
            PushButtonData pushButtonData1 = new PushButtonData("SplitCommandData", "Command Data", introLabPath, "HelloWorldCom");
            pushButtonData1.LargeImage = NewBitmapImage("ImgHelloWorld.png");
            pushButtonData1.AvailabilityClassName = "ButtonAvailability";

            // #2 
            PushButtonData pushButtonData2 = new PushButtonData("SplitDbElement", "DB Element", introLabPath, "HelloWorldCom");
            pushButtonData2.LargeImage = NewBitmapImage("ImgHelloWorld.png");
            pushButtonData2.AvailabilityClassName = "ButtonAvailability";

            // #3 
            PushButtonData pushButtonData3 = new PushButtonData("SplitElementFiltering", "ElementFiltering", introLabPath, "HelloWorldCom");
            pushButtonData3.LargeImage = NewBitmapImage("ImgHelloWorld.png");
            pushButtonData3.AvailabilityClassName = "ButtonAvailability";

            // Make a split button now 
            SplitButtonData splitBtnData = new SplitButtonData("SplitButton", "Split Button");

            SplitButton splitBtn = panel.AddItem(splitBtnData) as SplitButton;
            splitBtn.AddPushButton(pushButtonData1);
            splitBtn.AddPushButton(pushButtonData2);
            splitBtn.AddPushButton(pushButtonData3);
        }


        public void AddComboBox(RibbonPanel panel)
        {
            // Create five combo box members with two groups 

            // #1 
            ComboBoxMemberData comboBoxMemberData1 = new ComboBoxMemberData("ComboCommandData", "Command Data");
            comboBoxMemberData1.Image = NewBitmapImage("Basics.png");
            comboBoxMemberData1.GroupName = "DB Basics";

            // #2 
            ComboBoxMemberData comboBoxMemberData2 = new ComboBoxMemberData("ComboDbElement", "DB Element");
            comboBoxMemberData2.Image = NewBitmapImage("Basics.png");
            comboBoxMemberData2.GroupName = "DB Basics";

            // #3 
            ComboBoxMemberData comboBoxMemberData3 = new ComboBoxMemberData("ComboElementFiltering", "Filtering");
            comboBoxMemberData3.Image = NewBitmapImage("Basics.png");
            comboBoxMemberData3.GroupName = "DB Basics";

            // #4 
            ComboBoxMemberData comboBoxMemberData4 = new ComboBoxMemberData("ComboElementModification", "Modify");
            comboBoxMemberData4.Image = NewBitmapImage("Basics.png");
            comboBoxMemberData4.GroupName = "Modeling";

            // #5 
            ComboBoxMemberData comboBoxMemberData5 = new ComboBoxMemberData("ComboModelCreation", "Create");
            comboBoxMemberData5.Image = NewBitmapImage("Basics.png");
            comboBoxMemberData5.GroupName = "Modeling";

            // Make a combo box now 
            ComboBoxData comboBxData = new ComboBoxData("ComboBox");
            ComboBox comboBx = panel.AddItem(comboBxData) as ComboBox;
            comboBx.ToolTip = "Select an Option";
            comboBx.LongDescription = "select a command you want to run";
            comboBx.AddItem(comboBoxMemberData1);
            comboBx.AddItem(comboBoxMemberData2);
            comboBx.AddItem(comboBoxMemberData3);
            comboBx.AddItem(comboBoxMemberData4);
            comboBx.AddItem(comboBoxMemberData5);

            comboBx.CurrentChanged += new EventHandler<Autodesk.Revit.UI.Events.ComboBoxCurrentChangedEventArgs>(comboBx_CurrentChanged);
        }


        void comboBx_CurrentChanged(object sender, Autodesk.Revit.UI.Events.ComboBoxCurrentChangedEventArgs e)
        {
            // Cast sender as TextBox to retrieve text value
            ComboBox combodata = sender as ComboBox;
            ComboBoxMember member = combodata.Current;
            TaskDialog.Show("Combobox Selection", "Your new selection: " + member.ItemText);
        }


        public void AddRibbonSampler(UIControlledApplication app)
        {
            app.CreateRibbonTab("DoMagic");
            RibbonPanel panel = app.CreateRibbonPanel("DoMagic", "Создание файлов модели");
            AddPushButtons(panel);
            //AddSplitButton(panel);
            //AddComboBox(panel);
        }

        // OnStartup() - called when Revit starts. 
        public Autodesk.Revit.UI.Result OnStartup(Autodesk.Revit.UI.UIControlledApplication application)
        {
            // External application directory:
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // External command path:
            introLabPath = Path.Combine(dir, IntroLabName + DllExtension);

            if (!File.Exists(introLabPath))
            {
                TaskDialog.Show("UIRibbon", "External command assembly not found: " + introLabPath);
                return Result.Failed;
            }

            // Image path:
            imageFolder = FindFolderInParents(dir, ImageFolderName);

            if (null == imageFolder || !Directory.Exists(imageFolder))
            {
                TaskDialog.Show("UIRibbon", string.Format("No image folder named '{0}' found in the parent directories of '{1}.", ImageFolderName, dir));
                return Result.Failed;
            }

            AddRibbonSampler(application);
            return Result.Succeeded;
        }

        // OnShutdown() - called when Revit ends.  
        public Autodesk.Revit.UI.Result OnShutdown(Autodesk.Revit.UI.UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}