using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.IO;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
// [assembly: ESAPIScript(IsWriteable = true)]

namespace target_volume_extractor
{
    class VolumeSize
    {
        public string id;
        public string plan_id;
        public double x_len_mm;
        public double y_len_mm;
        public double z_len_mm;
        public double volume_cc;
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine($"arg: {arg}");
            }
            if (args.Length != 1)
            {
                Console.WriteLine($"ERROR: input argument as CSV input path is needed.");
                Console.ReadLine();
            }
            else
            {
                try
                {
                    using (Application app = Application.CreateApplication())
                    {

                        Console.WriteLine($"args[0]: {args[0]}");
                        Execute(app, args[0]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to Application.CreateApplication()");
                    Console.Error.WriteLine(e.ToString());
                    Console.ReadLine();
                }
            }
        }

        static void Execute(Application app, string input_path)
        {
            // TODO: Add your code here.

            const string course_id = "C1";
            List<(string, string)> id_plan_list = GetIdPlanNameFromCsv(input_path);
            List<VolumeSize> volsize_list = new List<VolumeSize>();

            string output_name = "volume_extractor_outputs.csv";
            string input_dir = Path.GetDirectoryName(input_path);
            string output_path = Path.Combine(input_dir, output_name);


            foreach (var (id, plan_id) in id_plan_list)
            {
                // for clinical ID mod
                string id_mod = (id.Length == 7) ? "0" + id : id;
                // end for clinical ID mod


                var patient = app.OpenPatientById(id_mod);
                if (patient == null)
                {
                    Console.WriteLine(String.Format("Patient (ID: {0}) is not opened.", id_mod));
                }
                else { 
                    Console.WriteLine(String.Format("Patient (ID: {0}) is opened.", id_mod));
                }

                var course = patient.Courses.Where(x => x.Id == course_id).FirstOrDefault();
                var plan = course.ExternalPlanSetups.Where(x => x.Id == plan_id).FirstOrDefault();

                var target_volume_id = plan.TargetVolumeID;
                var target_volume = plan.StructureSet.Structures.Where(x => x.Id == target_volume_id).FirstOrDefault();

                var mesh_geo = target_volume.MeshGeometry.Bounds;
                VolumeSize volsize = new VolumeSize
                {
                    id = id_mod,
                    plan_id = plan_id,
                    x_len_mm = mesh_geo.SizeX,
                    y_len_mm = mesh_geo.SizeY,
                    z_len_mm = mesh_geo.SizeZ,
                    volume_cc = target_volume.Volume
                };
                //                Console.WriteLine(string.Format("ID: {0}, PlanID: {1}, X: {2}, Y: {3}, Z: {4}, Volume: {5}", id, plan_id, x_len_mm, y_len_mm, z_len_mm, volume_cc));
                volsize_list.Add(volsize); 
                app.ClosePatient();

            }

            using (StreamWriter writer = new StreamWriter(output_path))
            {
                writer.WriteLine("# #1: ID, #2: Plan ID, #3: X_len_mm, #4: Y_len_mm, #5: Z_len_mm, #6: Volume_cc");

                foreach (var volsize in volsize_list)
                {
                    Console.WriteLine(string.Format("ID: {0}, PlanID: {1}, X: {2}, Y: {3}, Z: {4}, Volume: {5}"
                        , volsize.id
                        , volsize.plan_id
                        , volsize.x_len_mm
                        , volsize.y_len_mm
                        , volsize.z_len_mm
                        , volsize.volume_cc));

                    writer.WriteLine(string.Format("{0},{1},{2},{3},{4},{5}"
                        , volsize.id
                        , volsize.plan_id
                        , volsize.x_len_mm
                        , volsize.y_len_mm
                        , volsize.z_len_mm
                        , volsize.volume_cc));
                }
            }

            Console.WriteLine("End");
            Console.ReadLine();

        }

        static private List<(string, string)> GetIdPlanNameFromCsv(string csv_path)
        {

            const int id_col = 0;
            const int plan_col = 1;


            List<(string, string)> ret = new List<(string, string)>();

            using (var reader = new StreamReader(csv_path))
            {
                // skip header
                reader.ReadLine();

                // read body
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length > Math.Max(id_col, plan_col))
                    {
                        ret.Add((values[id_col], values[plan_col]));
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Short Line at {line}");
                    }
                }

            }

            return ret;

        }

    }
}
