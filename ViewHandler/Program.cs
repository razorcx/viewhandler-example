using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Model;

namespace ViewHandlerExample
{
	public class Program
	{
		public static void Main(string[] args)
		{
			ModelViewEnumerator viewEnum = ViewHandler.GetTemporaryViews();

			while (viewEnum.MoveNext())
			{
				var currentView = viewEnum.Current;
				ViewHandler.HideView(currentView);
			}

			var picker = new Picker();
			var connection = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_OBJECT) as Connection;

			if (connection == null) return;
			
			var origin = connection.GetCoordinateSystem().Origin;

			var minPoint = new Point
			{
				X = origin.X - 200,
				Y = origin.Y - 200,
				Z = origin.Z - 300
			};

			var maxPoint = new Point
			{
				X = origin.X + 200,
				Y = origin.Y + 200,
				Z = origin.Z + 300
			};
			var name = $"Connection: {connection.Name} ({connection.Identifier.ID})";
			var view = new View
			{
				Name = name,
				ViewCoordinateSystem =
				{
					AxisX = new Vector(1, 0, 0),
					AxisY = new Vector(0, 1, 0),
					Origin = origin
				},
				DisplayCoordinateSystem = new CoordinateSystem()
				{
					AxisX = new Vector(1, -.7, 0),
					AxisY = new Vector(0, 1, 2),
					Origin = origin
				},
				WorkArea =
				{
					MinPoint = minPoint,
					MaxPoint = maxPoint
				},
				ViewDepthUp = 500,
				ViewDepthDown = 500,
			};
			view.Insert();

			var views = ViewHandler.GetAllViews();
			while (views.MoveNext())
			{
				var currentView = views.Current;
				if (currentView.Name == name)
				{
					ViewHandler.ShowView(currentView);
				}
			}
		}
	}
}
