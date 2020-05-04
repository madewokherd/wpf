using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;

class Xaml2Cs
{
	public Xaml2Cs ()
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

		types = new Dictionary<string,XamlType>();

		types["AnimationTimeline"] = new XamlType("System.Windows.Media.Animation", "AnimationTimeline");
		types["BeginStoryboard"] = new XamlType("System.Windows.Media.Animation", "BeginStoryboard");
		types["Binding"] = new XamlType("System.Windows.Data", "Binding");
		types["Border"] = new XamlType("System.Windows.Controls", "Border");
		types["Brush"] = new XamlType("System.Windows.Media", "Brush");
		types["Button"] = new XamlType("System.Windows.Controls", "Button");
		types["ButtonBase"] = new XamlType("System.Windows.Controls.Primitives", "ButtonBase");
		types["Canvas"] = new XamlType("System.Windows.Controls", "Canvas");
		types["Color"] = new XamlType("System.Windows.Media", "Color");
		types["ColorAnimation"] = new XamlType("System.Windows.Media.Animation", "ColorAnimation");
		types["ColorAnimationBase"] = new XamlType("System.Windows.Media.Animation", "ColorAnimationBase");
		types["ColumnDefinition"] = new XamlType("System.Windows.Controls", "ColumnDefinition");
		types["ColumnDefinitionCollection"] = new XamlType("System.Windows.Controls", "ColumnDefinitionCollection");
		types["ComponentResourceKey"] = new XamlType("System.Windows", "ComponentResourceKey");
		types["Condition"] = new XamlType("System.Windows", "Condition");
		types["ConditionCollection"] = new XamlType("System.Windows", "ConditionCollection");
		types["ContentControl"] = new XamlType("System.Windows.Controls", "ContentControl");
		types["ContentPresenter"] = new XamlType("System.Windows.Controls", "ContentPresenter");
		types["Control"] = new XamlType("System.Windows.Controls", "Control");
		types["ControlTemplate"] = new XamlType("System.Windows.Controls", "ControlTemplate");
		types["ControlTemplate"].is_template = true;
		types["CornerRadius"] = new XamlType("System.Windows", "CornerRadius");
		types["Decorator"] = new XamlType("System.Windows.Controls", "Decorator");
		types["DependencyProperty"] = new XamlType("System.Windows", "DependencyProperty");
		types["double"] = new XamlType(null, "double");
		types["DoubleAnimation"] = new XamlType("System.Windows.Media.Animation", "DoubleAnimation");
		types["DoubleAnimationBase"] = new XamlType("System.Windows.Media.Animation", "DoubleAnimationBase");
		types["Duration"] = new XamlType("System.Windows", "Duration");
		types["event"] = new XamlType(null, "event");
		types["Ellipse"] = new XamlType("System.Windows.Shapes", "Ellipse");
		types["EventTrigger"] = new XamlType("System.Windows", "EventTrigger");
		types["FocusManager"] = new XamlType("System.Windows.Input", "FocusManager");
		types["FontFamily"] = new XamlType("System.Windows.Media", "FontFamily");
		types["FontStyle"] = new XamlType("System.Windows", "FontStyle");
		types["FontWeight"] = new XamlType("System.Windows", "FontWeight");
		types["FrameworkContentElement"] = new XamlType("System.Windows", "FrameworkContentElement");
		types["FrameworkElement"] = new XamlType("System.Windows", "FrameworkElement");
		types["FrameworkTemplate"] = new XamlType("System.Windows", "FrameworkTemplate");
		types["Geometry"] = new XamlType("System.Windows.Media", "Geometry");
		types["GradientBrush"] = new XamlType("System.Windows.Media", "GradientBrush");
		types["GradientStop"] = new XamlType("System.Windows.Media", "GradientStop");
		types["GradientStopCollection"] = new XamlType("System.Windows.Media", "GradientStopCollection");
		types["Grid"] = new XamlType("System.Windows.Controls", "Grid");
		types["GridLength"] = new XamlType("System.Windows", "GridLength");
		types["HeaderedItemsControl"] = new XamlType("System.Windows.Controls", "HeaderedItemsControl");
		types["Hyperlink"] = new XamlType("System.Windows.Documents", "Hyperlink");
		types["IList"] = new XamlType("System.Collections", "IList");
		types["Inline"] = new XamlType("System.Windows.Documents", "Inline");
		types["InlineCollection"] = new XamlType("System.Windows.Documents", "InlineCollection");
		types["int"] = new XamlType(null, "int");
		types["ItemsControl"] = new XamlType("System.Windows.Controls", "ItemsControl");
		types["ItemsPresenter"] = new XamlType("System.Windows.Controls", "ItemsPresenter");
		types["KeyboardNavigation"] = new XamlType("System.Windows.Input", "KeyboardNavigation");
		types["Label"] = new XamlType("System.Windows.Controls", "Label");
		types["LinearGradientBrush"] = new XamlType("System.Windows.Media", "LinearGradientBrush");
		types["Menu"] = new XamlType("System.Windows.Controls", "Menu");
		types["MenuBase"] = new XamlType("System.Windows.Controls.Primitives", "MenuBase");
		types["MenuItem"] = new XamlType("System.Windows.Controls", "MenuItem");
		types["MultiTrigger"] = new XamlType("System.Windows.Markup", "MultiTrigger");
		types["object"] = new XamlType(null, "object");
		types["Orientation"] = new XamlType("System.Windows.Controls", "Orientation");
		types["Orientation"].is_enum = true;
		types["Page"] = new XamlType("System.Windows.Controls", "Page");
		types["Panel"] = new XamlType("System.Windows.Controls", "Panel");
		types["ParallelTimeline"] = new XamlType("System.Windows.Media.Animation", "ParallelTimeline");
		types["Path"] = new XamlType("System.Windows.Shapes", "Path");
		types["PenLineCap"] = new XamlType("System.Windows.Media", "PenLineCap");
		types["PenLineCap"].is_enum = true;
		types["PlacementMode"] = new XamlType("System.Windows.Controls.Primitives", "PlacementMode");
		types["PlacementMode"].is_enum = true;
		types["Point"] = new XamlType("System.Windows", "Point");
		types["Popup"] = new XamlType("System.Windows.Controls.Primitives", "Popup");
		types["PopupAnimation"] = new XamlType("System.Windows.Controls.Primitives", "PopupAnimation");
		types["PopupAnimation"].is_enum = true;
		types["ProgressBar"] = new XamlType("System.Windows.Controls", "ProgressBar");
		types["PropertyPath"] = new XamlType("System.Windows", "PropertyPath");
		types["RelativeSource"] = new XamlType("System.Windows.Data", "RelativeSource");
		types["RangeBase"] = new XamlType("System.Windows.Controls.Primitives", "RangeBase");
		types["ResourceDictionary"] = new XamlType("System.Windows", "ResourceDictionary");
		types["RoutedEvent"] = new XamlType("System.Windows", "RoutedEvent");
		types["RowDefinition"] = new XamlType("System.Windows.Controls", "RowDefinition");
		types["RowDefinitionCollection"] = new XamlType("System.Windows.Controls", "RowDefinitionCollection");
		types["ScaleTransform"] = new XamlType("System.Windows.Media", "ScaleTransform");
		types["ScrollViewer"] = new XamlType("System.Windows.Controls", "ScrollViewer");
		types["Setter"] = new XamlType("System.Windows", "Setter");
		types["Shape"] = new XamlType("System.Windows.Shapes", "Shape");
		types["SolidColorBrush"] = new XamlType("System.Windows.Media", "SolidColorBrush");
		types["Span"] = new XamlType("System.Windows.Documents", "Span");
		types["StackPanel"] = new XamlType("System.Windows.Controls", "StackPanel");
		types["Storyboard"] = new XamlType("System.Windows.Media.Animation", "Storyboard");
		types["string"] = new XamlType(null, "string");
		types["Stretch"] = new XamlType("System.Windows.Media", "Stretch");
		types["Stretch"].is_enum = true;
		types["Style"] = new XamlType("System.Windows", "Style");
		types["TextBlock"] = new XamlType("System.Windows.Controls", "TextBlock");
		types["TextBoxBase"] = new XamlType("System.Windows.Controls.Primitives", "TextBoxBase");
		types["TextBox"] = new XamlType("System.Windows.Controls.Primitives", "TextBox");
		types["TextElement"] = new XamlType("System.Windows.Documents", "TextElement");
		types["TextTrimming"] = new XamlType("System.Windows", "TextTrimming");
		types["TextTrimming"].is_enum = true;
		types["TextWrapping"] = new XamlType("System.Windows", "TextWrapping");
		types["TextWrapping"].is_enum = true;
		types["Thickness"] = new XamlType("System.Windows", "Thickness");
		types["Timeline"] = new XamlType("System.Windows.Media.Animation", "Timeline");
		types["TimelineCollection"] = new XamlType("System.Windows.Media.Animation", "TimelineCollection");
		types["TimelineGroup"] = new XamlType("System.Windows.Media.Animation", "TimelineGroup");
		types["ToolBar"] = new XamlType("System.Windows.Controls", "ToolBar");
		types["ToolBarTray"] = new XamlType("System.Windows.Controls", "ToolBarTray");
		types["ToolTipService"] = new XamlType("System.Windows.Controls", "ToolTipService");
		types["Transform"] = new XamlType("System.Windows.Media", "Transform");
		types["Trigger"] = new XamlType("System.Windows", "Trigger");
		types["TriggerAction"] = new XamlType("System.Windows", "TriggerAction");
		types["TriggerBase"] = new XamlType("System.Windows", "TriggerBase");
		types["TriggerCollection"] = new XamlType("System.Windows", "TriggerCollection");
		types["Type"] = new XamlType("System", "Type");
		types["UIElement"] = new XamlType("System.Windows", "UIElement");
		types["Uri"] = new XamlType("System", "Uri");
		types["Visibility"] = new XamlType("System.Windows", "Visibility");
		types["Visibility"].is_enum = true;
		types["bool"] = new XamlType(null, "bool");
		types["HorizontalAlignment"] = new XamlType("System.Windows", "HorizontalAlignment");
		types["HorizontalAlignment"].is_enum = true;
		types["KeyboardNavigationMode"] = new XamlType("System.Windows.Input", "KeyboardNavigationMode");
		types["KeyboardNavigationMode"].is_enum = true;
		types["VerticalAlignment"] = new XamlType("System.Windows", "VerticalAlignment");
		types["VerticalAlignment"].is_enum = true;

		types["AnimationTimeline"].base_type = types["Timeline"];
		types["BeginStoryboard"].base_type = types["TriggerAction"];
		types["Border"].base_type = types["Decorator"];
		types["Button"].base_type = types["ButtonBase"];
		types["ButtonBase"].base_type = types["ContentControl"];
		types["Canvas"].base_type = types["Panel"];
		types["ColorAnimation"].base_type = types["ColorAnimationBase"];
		types["ColorAnimationBase"].base_type = types["AnimationTimeline"];
		types["ContentControl"].base_type = types["Control"];
		types["ContentPresenter"].base_type = types["FrameworkElement"];
		types["Control"].base_type = types["FrameworkElement"];
		types["ControlTemplate"].base_type = types["FrameworkTemplate"];
		types["Decorator"].base_type = types["FrameworkElement"];
		types["DoubleAnimation"].base_type = types["DoubleAnimationBase"];
		types["DoubleAnimationBase"].base_type = types["AnimationTimeline"];
		types["Ellipse"].base_type = types["Shape"];
		types["EventTrigger"].base_type = types["TriggerBase"];
		types["FrameworkElement"].base_type = types["UIElement"];
		types["GradientBrush"].base_type = types["Brush"];
		types["Grid"].base_type = types["Panel"];
		types["HeaderedItemsControl"].base_type = types["ItemsControl"];
		types["Hyperlink"].base_type = types["Span"];
		types["Inline"].base_type = types["TextElement"];
		types["ItemsControl"].base_type = types["Control"];
		types["ItemsPresenter"].base_type = types["FrameworkElement"];
		types["Label"].base_type = types["ContentControl"];
		types["LinearGradientBrush"].base_type = types["GradientBrush"];
		types["Menu"].base_type = types["MenuBase"];
		types["MenuBase"].base_type = types["ItemsControl"];
		types["MenuItem"].base_type = types["HeaderedItemsControl"];
		types["Page"].base_type = types["FrameworkElement"];
		types["Panel"].base_type = types["FrameworkElement"];
		types["ParallelTimeline"].base_type = types["TimelineGroup"];
		types["Path"].base_type = types["Shape"];
		types["Popup"].base_type = types["FrameworkElement"];
		types["ProgressBar"].base_type = types["RangeBase"];
		types["RangeBase"].base_type = types["Control"];
		types["ScaleTransform"].base_type = types["Transform"];
		types["ScrollViewer"].base_type = types["ContentControl"];
		types["Shape"].base_type = types["FrameworkElement"];
		types["Span"].base_type = types["Inline"];
		types["StackPanel"].base_type = types["Panel"];
		types["Storyboard"].base_type = types["ParallelTimeline"];
		types["TextBlock"].base_type = types["FrameworkElement"];
		types["TextBoxBase"].base_type = types["Control"];
		types["TextBox"].base_type = types["TextBoxBase"];
		types["TextElement"].base_type = types["FrameworkContentElement"];
		types["TimelineGroup"].base_type = types["Timeline"];
		types["ToolBar"].base_type = types["HeaderedItemsControl"];

		types["BeginStoryboard"].add_statement = "{0}.Storyboard = {1};";
		types["ColumnDefinitionCollection"].add_statement = "{0}.Add({1});";
		types["ConditionCollection"].add_statement = "{0}.Add({1});";
		types["EventTrigger"].add_statement = "{0}.Actions.Add({1});";
		types["FrameworkTemplate"].add_statement = "{0}.VisualTree.AppendChild({1});";
		types["GradientStopCollection"].add_statement = "{0}.Add({1});";
		types["IList"].add_statement = "{0}.Add({1});";
		types["InlineCollection"].add_statement = "{0}.Add({1});";
		types["ItemsControl"].add_statement = "{0}.Items.Add({1});";
		types["MultiTrigger"].add_statement = "{0}.Setters.Add({1});";
		types["Panel"].add_statement = "{0}.Children.Add({1});";
		types["ResourceDictionary"].add_with_key_statement = "{0}.Add({1}, {2});";
		types["RowDefinitionCollection"].add_statement = "{0}.Add({1});";
		types["Style"].add_statement = "{0}.Setters.Add({1});";
		types["TimelineCollection"].add_statement = "{0}.Add({1});";
		types["Trigger"].add_statement = "{0}.Setters.Add({1});";
		types["TriggerCollection"].add_statement = "{0}.Add({1});";

		types["Binding"].AddProperty(types["string"], "ElementName", false);
		types["Binding"].AddProperty(types["PropertyPath"], "Path", true);
		types["Binding"].AddProperty(types["RelativeSource"], "RelativeSource", true);
		types["Border"].AddProperty(types["Brush"], "Background", true);
		types["Border"].AddProperty(types["Brush"], "BorderBrush", true);
		types["Border"].AddProperty(types["Thickness"], "BorderThickness", true);
		types["Border"].AddProperty(types["CornerRadius"], "CornerRadius", true);
		types["Border"].AddProperty(types["Thickness"], "Padding", true);
		types["ButtonBase"].AddProperty(types["event"], "Click", false);
		types["Canvas"].AddProperty(types["double"], "Bottom", true);
		types["Canvas"].AddProperty(types["double"], "Left", true);
		types["Canvas"].AddProperty(types["double"], "Right", true);
		types["Canvas"].AddProperty(types["double"], "Top", true);
		types["ColorAnimation"].AddProperty(types["Color"], "From", true);
		types["ColorAnimation"].AddProperty(types["Color"], "To", true);
		types["ColumnDefinition"].AddProperty(types["GridLength"], "Width", true);
		types["ComponentResourceKey"].AddProperty(types["object"], "ResourceId", false);
		types["ComponentResourceKey"].AddProperty(types["Type"], "TypeInTargetAssembly", false);
		types["Condition"].AddProperty(types["DependencyProperty"], "Property", false);
		types["Condition"].AddProperty(types["object"], "Value", false);
		types["Condition"].props["Value"].indirect_property = true;
		types["ContentControl"].AddProperty(types["object"], "Content", true);
		types["Control"].AddProperty(types["Brush"], "Background", true);
		types["Control"].AddProperty(types["Brush"], "BorderBrush", true);
		types["Control"].AddProperty(types["Thickness"], "BorderThickness", true);
		types["Control"].AddProperty(types["FontStyle"], "FontStyle", true);
		types["Control"].AddProperty(types["Brush"], "Foreground", true);
		types["Control"].AddProperty(types["HorizontalAlignment"], "HorizontalContentAlignment", true);
		types["Control"].AddProperty(types["bool"], "IsTabStop", true);
		types["Control"].AddProperty(types["Thickness"], "Padding", true);
		types["ControlTemplate"].AddProperty(types["Type"], "TargetType", false);
		types["ControlTemplate"].AddProperty(types["TriggerCollection"], "Triggers", false);
		types["ControlTemplate"].props["Triggers"].auto = true;
		types["Decorator"].AddProperty(types["UIElement"], "Child", false);
		types["DoubleAnimation"].AddProperty(types["double"], "From", true);
		types["DoubleAnimation"].AddProperty(types["double"], "To", true);
		types["EventTrigger"].AddProperty(types["RoutedEvent"], "RoutedEvent", false);
		types["EventTrigger"].AddProperty(types["string"], "SourceName", false);
		types["FocusManager"].AddProperty(types["FrameworkElement"], "FocusedElement", true);
		types["FocusManager"].AddProperty(types["bool"], "IsFocusScope", true);
		types["FrameworkContentElement"].AddProperty(types["string"], "Name", true);
		types["FrameworkElement"].AddProperty(types["double"], "Height", true);
		types["FrameworkElement"].AddProperty(types["HorizontalAlignment"], "HorizontalAlignment", false);
		types["FrameworkElement"].AddProperty(types["Thickness"], "Margin", true);
		types["FrameworkElement"].AddProperty(types["double"], "MaxWidth", true);
		types["FrameworkElement"].AddProperty(types["double"], "MinHeight", true);
		types["FrameworkElement"].AddProperty(types["double"], "MinWidth", true);
		types["FrameworkElement"].AddProperty(types["string"], "Name", true);
		types["FrameworkElement"].AddProperty(types["ResourceDictionary"], "Resources", false);
		types["FrameworkElement"].props["Resources"].auto = true;
		types["FrameworkElement"].AddProperty(types["Style"], "Style", true);
		types["FrameworkElement"].AddProperty(types["object"], "ToolTip", true);
		types["FrameworkElement"].AddProperty(types["TriggerCollection"], "Triggers", false);
		types["FrameworkElement"].props["Triggers"].auto = true;
		types["FrameworkElement"].AddProperty(types["VerticalAlignment"], "VerticalAlignment", false);
		types["FrameworkElement"].AddProperty(types["double"], "Width", true);
		types["GradientBrush"].AddProperty(types["GradientStopCollection"], "GradientStops", false);
		types["GradientBrush"].props["GradientStops"].auto = true;
		types["GradientStop"].AddProperty(types["Color"], "Color", false);
		types["GradientStop"].AddProperty(types["double"], "Offset", false);
		types["Grid"].AddProperty(types["int"], "Column", true);
		types["Grid"].AddProperty(types["ColumnDefinitionCollection"], "ColumnDefinitions", false);
		types["Grid"].props["ColumnDefinitions"].auto = true;
		types["Grid"].AddProperty(types["int"], "Row", true);
		types["Grid"].AddProperty(types["RowDefinitionCollection"], "RowDefinitions", false);
		types["Grid"].props["RowDefinitions"].auto = true;
		types["HeaderedItemsControl"].AddProperty(types["object"], "Header", true);
		types["KeyboardNavigation"].AddProperty(types["KeyboardNavigationMode"], "DirectionalNavigation", true);
		types["KeyboardNavigation"].AddProperty(types["KeyboardNavigationMode"], "TabNavigation", true);
		types["LinearGradientBrush"].AddProperty(types["Point"], "EndPoint", false);
		types["LinearGradientBrush"].AddProperty(types["Point"], "StartPoint", false);
		types["Menu"].AddProperty(types["bool"], "IsMainMenu", true);
		types["MenuItem"].AddProperty(types["bool"], "IsCheckable", true);
		types["MultiTrigger"].AddProperty(types["ConditionCollection"], "Conditions", false);
		types["MultiTrigger"].props["Conditions"].auto = true;
		types["object"].AddProperty(types["object"], "_textcontent", false);
		types["object"].AddProperty(types["object"], "_key", false);
		types["object"].AddProperty(types["object"], "_dynamicresource", false);
		types["Page"].AddProperty(types["Brush"], "Background", true);
		types["Page"].AddProperty(types["object"], "Content", true);
		types["Panel"].AddProperty(types["Brush"], "Background", true);
		types["Path"].AddProperty(types["Geometry"], "Data", true);
		types["Popup"].AddProperty(types["bool"], "AllowsTransparency", true);
		types["Popup"].AddProperty(types["bool"], "IsOpen", true);
		types["Popup"].AddProperty(types["PlacementMode"], "Placement", true);
		types["Popup"].AddProperty(types["PopupAnimation"], "PopupAnimation", true);
		types["Popup"].AddProperty(types["double"], "VerticalOffset", true);
		types["RangeBase"].AddProperty(types["double"], "Value", true);
		types["ResourceDictionary"].AddProperty(types["IList"], "MergedDictionaries", false);
		types["ResourceDictionary"].props["MergedDictionaries"].auto = true;
		types["ResourceDictionary"].AddProperty(types["Uri"], "Source", false);
		types["RowDefinition"].AddProperty(types["GridLength"], "Height", true);
		types["ScaleTransform"].AddProperty(types["double"], "ScaleX", true);
		types["ScaleTransform"].AddProperty(types["double"], "ScaleY", true);
		types["ScrollViewer"].AddProperty(types["bool"], "CanContentScroll", true);
		types["Setter"].AddProperty(types["DependencyProperty"], "Property", false);
		types["Setter"].AddProperty(types["string"], "TargetName", false);
		types["Setter"].AddProperty(types["object"], "Value", false);
		types["Setter"].props["Value"].indirect_property = true;
		types["Shape"].AddProperty(types["Brush"], "Fill", true);
		types["Shape"].AddProperty(types["Stretch"], "Stretch", true);
		types["Shape"].AddProperty(types["Brush"], "Stroke", true);
		types["Shape"].AddProperty(types["PenLineCap"], "StrokeEndLineCap", true);
		types["Shape"].AddProperty(types["PenLineCap"], "StrokeStartLineCap", true);
		types["Shape"].AddProperty(types["double"], "StrokeThickness", true);
		types["SolidColorBrush"].AddProperty(types["Color"], "Color", false);
		types["Span"].AddProperty(types["InlineCollection"], "Inlines", true);
		types["Span"].props["Inlines"].auto = true;
		types["StackPanel"].AddProperty(types["Orientation"], "Orientation", true);
		types["Storyboard"].AddProperty(types["string"], "TargetName", true);
		types["Storyboard"].AddProperty(types["PropertyPath"], "TargetProperty", true);
		types["Style"].AddProperty(types["Style"], "BasedOn", false);
		types["Style"].AddProperty(types["Type"], "TargetType", false);
		types["Style"].AddProperty(types["object"], "Value", false);
		types["Style"].props["Value"].indirect_property = true;
		types["TextBlock"].AddProperty(types["InlineCollection"], "Inlines", true);
		types["TextBlock"].props["Inlines"].auto = true;
		types["TextBlock"].AddProperty(types["FontFamily"], "FontFamily", true);
		types["TextBlock"].AddProperty(types["FontWeight"], "FontWeight", true);
		types["TextBlock"].AddProperty(types["Brush"], "Foreground", true);
		types["TextBlock"].AddProperty(types["double"], "FontSize", true);
		types["TextBlock"].AddProperty(types["TextTrimming"], "TextTrimming", true);
		types["TextBlock"].AddProperty(types["TextWrapping"], "TextWrapping", true);
		types["TextBoxBase"].AddProperty(types["event"], "TextChanged", false);
		types["TextBox"].AddProperty(types["int"], "MaxLength", true);
		types["TextElement"].AddProperty(types["Brush"], "Foreground", true);
		types["Timeline"].AddProperty(types["bool"], "AutoReverse", true);
		types["Timeline"].AddProperty(types["Duration"], "Duration", true);
		types["TimelineGroup"].AddProperty(types["TimelineCollection"], "Children", true);
		types["ToolBarTray"].AddProperty(types["bool"], "IsLocked", true);
		types["ToolTipService"].AddProperty(types["bool"], "ShowOnDisabled", true);
		types["Trigger"].AddProperty(types["DependencyProperty"], "Property", false);
		types["Trigger"].AddProperty(types["object"], "Value", false);
		types["UIElement"].AddProperty(types["bool"], "AllowDrop", true);
		types["UIElement"].AddProperty(types["bool"], "Focusable", true);
		types["UIElement"].AddProperty(types["bool"], "IsEnabled", true);
		types["UIElement"].AddProperty(types["bool"], "IsKeyboardFocused", true);
		types["UIElement"].AddProperty(types["bool"], "IsMouseOver", true);
		types["UIElement"].AddProperty(types["double"], "Opacity", true);
		types["UIElement"].AddProperty(types["Brush"], "OpacityMask", true);
		types["UIElement"].AddProperty(types["event"], "PreviewKeyDown", false);
		types["UIElement"].AddProperty(types["Transform"], "RenderTransform", true);
		types["UIElement"].AddProperty(types["bool"], "SnapsToDevicePixels", true);
		types["UIElement"].AddProperty(types["Visibility"], "Visibility", true);

		types["ContentControl"].content_prop = types["ContentControl"].props["Content"];
		types["Decorator"].content_prop = types["Decorator"].props["Child"];
		types["GradientBrush"].content_prop = types["GradientBrush"].props["GradientStops"];
		types["Page"].content_prop = types["Page"].props["Content"];
		types["Span"].content_prop = types["Span"].props["Inlines"];
		types["TextBlock"].content_prop = types["TextBlock"].props["Inlines"];
		types["TimelineGroup"].content_prop = types["TimelineGroup"].props["Children"];

		elements_by_local = new Dictionary<string,XamlElement>();
		elements_by_name = new Dictionary<string,XamlElement>();
		static_resources = new Dictionary<string,XamlElement>();
	}

	class XamlType
	{
		public string ns;
		public string name;
		public Dictionary<string, XamlProperty> props = new Dictionary<string, XamlProperty>();
		public bool is_enum;
		public XamlType base_type;
		public string add_with_key_statement;
		public string add_statement;
		public bool is_template;
		public XamlProperty content_prop;

		public XamlType(string ns, string name)
		{
			this.ns = ns;
			this.name = name;
		}

		public void AddProperty(XamlType value_type, string name, bool dependency)
		{
			var new_prop = new XamlProperty();
			new_prop.name = name;
			new_prop.container_type = this;
			new_prop.value_type = value_type;
			new_prop.dependency = dependency;
			this.props[name] = new_prop;
		}

		public bool SubclassOf(XamlType other)
		{
			if (other.name == "object")
				return true;
			XamlType mine = this;
			while (mine != null)
			{
				if (mine == other)
					return true;
				mine = mine.base_type;
			}
			return false;
		}

		public bool LookupProp(string name, out XamlProperty prop)
		{
			XamlType container_type = this;
			while (container_type != null)
			{
				if (container_type.props.TryGetValue(name, out prop))
					return true;
				container_type = container_type.base_type;
			}
			prop = null;
			return false;
		}

		public string AddWithKeyStatement(string parent_expr, string key_expr, string value_expr)
		{
			XamlType parent = this;
			while (parent.add_with_key_statement == null)
			{
				if (parent.base_type == null)
					throw new NotImplementedException(String.Format("add with key for {0}", name));
				parent = parent.base_type;
			}
			return String.Format(parent.add_with_key_statement, parent_expr, key_expr, value_expr);
		}

		public XamlProperty ContentProp
		{
			get
			{
				XamlType base_type = this;
				while (base_type != null)
				{
					if (base_type.content_prop != null)
						return base_type.content_prop;
					base_type = base_type.base_type;
				}
				throw new NotImplementedException(String.Format("content property for {0}", name));
			}
		}

		public bool IsCollection
		{
			get
			{
				return add_statement != null || (base_type != null && base_type.IsCollection);
			}
		}

		public string AddStatement(XamlElement element, string parent_expr, string value_expr)
		{
			XamlType parent = this;
			while (true)
			{
				if (parent.add_statement != null)
					return String.Format(parent.add_statement, parent_expr, value_expr);
				if (parent.content_prop != null)
				{
					var prop = parent.content_prop;
					if (prop.value_type.IsCollection)
					{
						string result = prop.value_type.AddStatement(element,
							String.Format("({0}).{1}", parent_expr, prop.name),
							value_expr);
						if (!prop.auto && !element.parent.initialized_content)
						{
							element.parent.early_init.Add(String.Format(
								"{0}.{1} = new {2}();",
								parent_expr, prop.name, prop.value_type.name));
							element.parent.initialized_content = true;
						}
						return result;
					}
					else
					{
						return String.Format("({0}).{1} = {2};",
							parent_expr, prop.name, value_expr);
					}
				}
				if (parent.base_type == null)
					throw new NotImplementedException(String.Format("add child for {0}", name));
				parent = parent.base_type;
			}
		}
	}

	class XamlProperty
	{
		public string name;
		public XamlType container_type;
		public XamlType value_type;
		public bool dependency;
		public bool auto;
		public bool indirect_property;
	}

	Dictionary<string, XamlType> types;

	HashSet<string> namespaces = new HashSet<string>();

	class XamlElement
	{
		public XamlType type;
		public XamlElement parent;
		public List<XamlElement> children = new List<XamlElement>();
		public string local_name;
		public string class_name;
		public string class_modifier = "";
		public string name;
		public List<string> early_init = new List<string>();
		public List<string> late_init = new List<string>();
		public List<string> class_decls = new List<string>();
		public XamlProperty prop;
		public bool has_attributes;
		public string key;
		public string key_expr;
		public XamlType target_type;
		public string setter_property;
		public bool is_template;
		public bool initialized_content;
	}

	XamlElement root_element;
	
	Dictionary<string, XamlElement> elements_by_local;

	Dictionary<string, XamlElement> elements_by_name;
	
	Dictionary<string, XamlElement> static_resources;

	internal static string quote_string(string str)
	{
		return String.Format("\"{0}\"",
			str.Replace("\\", "\\\\"));
	}

	string attribute_string_to_expression(XamlElement element, XamlProperty prop, string str)
	{
		if (prop.name == "Property")
		{
			element.setter_property = str;
		}
		if (prop.container_type.name == "Setter"
			&& prop.name == "TargetName")
		{
			element.target_type = elements_by_name[str].type;
		}

		string value_expression;
		if (str.StartsWith("{DynamicResource ") && str.EndsWith("}"))
		{
			value_expression = String.Format("({0})this.FindResource({1})",
				prop.value_type.name,
				attribute_string_to_expression(element, types["object"].props["_dynamicresource"],
					str.Substring(17, str.Length - 18)));
		}
		else if (str.StartsWith("{StaticResource ") && str.EndsWith("}"))
		{
			string key = str.Substring(16, str.Length - 17);
			value_expression = static_resources[key].local_name;
		}
		else if (str.StartsWith("{x:Static ") && str.EndsWith("}"))
		{
			value_expression = str.Substring(10, str.Length - 11);
		}
		else if (str.StartsWith("{x:Type ") && str.EndsWith("}"))
		{
			string type_name = str.Substring(8, str.Length - 9);
			XamlType type;
			if (!types.TryGetValue(type_name, out type))
				throw new NotImplementedException(String.Format("type {0}", type_name));
			if (type.ns != null)
				namespaces.Add(type.ns);
			value_expression = String.Format("typeof({0})", type.name);
			if (prop.name == "TargetType")
				element.target_type = type;
		}
		else if (str == "{x:Null}")
		{
			value_expression = "null";
		}
		else if (str.StartsWith("{Binding ") && str.EndsWith("}"))
		{
			namespaces.Add("System.Windows.Data");
			string attributes_str = str.Substring(9, str.Length - 10);
			var initializers = new List<string>();
			XamlType binding_type = types["Binding"];
			foreach (string prop_str in attributes_str.Split(','))
			{
				string trimmed_str = prop_str.Trim();
				string[] parts = trimmed_str.Split(new char[] {'='}, 2);
				XamlProperty binding_prop;
				if (!binding_type.LookupProp(parts[0], out binding_prop))
					throw new NotImplementedException(string.Format("Binding property {0}", parts[0]));
				string prop_val_expr = attribute_string_to_expression(null, binding_prop, parts[1]);
				initializers.Add(String.Format("{0} = {1}, ", parts[0], prop_val_expr));
			}
			return String.Format("new Binding{{ {0} }}", String.Join("", initializers));
		}
		else if (str.StartsWith("{TemplateBinding ") && str.EndsWith("}"))
		{
			string binding_target = str.Substring(17, str.Length - 18);
			XamlType binding_type = types["Binding"];
			return String.Format("new Binding{{ RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent), Path = new PropertyPath({0}), }}", quote_string(binding_target));
		}
		else if (str.StartsWith("{RelativeSource ") && str.EndsWith("}"))
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			string contents = str.Substring(16, str.Length - 17);
			if (contents == "TemplatedParent")
			{
				value_expression = String.Format("new RelativeSource(RelativeSourceMode.{0})", contents);
			}
			else
			{
				throw new NotImplementedException(String.Format("RelativeSource {0}", contents));
			}
		}
		else if (str.StartsWith("{ComponentResourceKey ") && str.EndsWith("}"))
		{
			string attributes_str = str.Substring(22, str.Length - 23);
			var initializers = new List<string>();
			XamlType result_type = types["ComponentResourceKey"];
			foreach (string prop_str in attributes_str.Split(','))
			{
				string trimmed_str = prop_str.Trim();
				string[] parts = trimmed_str.Split(new char[] {'='}, 2);
				XamlProperty result_prop;
				if (!result_type.LookupProp(parts[0], out result_prop))
					throw new NotImplementedException(string.Format("ComponentResourceKey property {0}", parts[0]));
				string prop_val_expr = attribute_string_to_expression(null, result_prop, parts[1]);
				initializers.Add(String.Format("{0} = {1}, ", parts[0], prop_val_expr));
			}
			return String.Format("new ComponentResourceKey{{ {0} }}", String.Join("", initializers));
		}
		else if (prop.indirect_property)
		{
			XamlType actual_type;
			XamlProperty actual_prop;
			actual_type = element.target_type;
			if (!actual_type.LookupProp(element.setter_property, out actual_prop))
				throw new NotImplementedException(String.Format("property {0}", element.setter_property));
			return attribute_string_to_expression(element, actual_prop, str);
		}
		else if (str.StartsWith("{"))
		{
			throw new Exception(String.Format("failed converting property value {0}", str));
		}
		else if (prop.value_type.name == "bool" &&
			String.Compare(str, "True", true) == 0)
		{
			value_expression = "true";
		}
		else if (prop.value_type.name == "bool" &&
			String.Compare(str, "False", true) == 0)
		{
			value_expression = "false";
		}
		else if (prop.value_type.name == "VerticalAlignment" &&
			str.ToLowerInvariant() == "center")
		{
			value_expression = "VerticalAlignment.Center";
		}
		else if (prop.value_type.name == "HorizontalAlignment" &&
			str.ToLowerInvariant() == "center")
		{
			value_expression = "HorizontalAlignment.Center";
		}
		else if (prop.value_type.is_enum)
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = String.Format("{0}.{1}", prop.value_type.name, str);
		}
		else if (prop.value_type.name == "DependencyProperty")
		{
			XamlType target_type = element.target_type;
			value_expression = String.Format("{0}.{1}Property", target_type.name, str);
		}
		else if (prop.value_type.name == "Color" && str.StartsWith("#") && str.Length == 9)
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			byte a, r, g, b;
			a = Convert.ToByte(str.Substring(1, 2), 16);
			r = Convert.ToByte(str.Substring(3, 2), 16);
			g = Convert.ToByte(str.Substring(5, 2), 16);
			b = Convert.ToByte(str.Substring(7, 2), 16);
			value_expression = String.Format("Color.FromArgb({0}, {1}, {2}, {3})", a, r, g, b);
		}
		else if (prop.value_type.name == "Color" && str.StartsWith("#") && str.Length == 7)
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			byte r, g, b;
			r = Convert.ToByte(str.Substring(1, 2), 16);
			g = Convert.ToByte(str.Substring(3, 2), 16);
			b = Convert.ToByte(str.Substring(5, 2), 16);
			value_expression = String.Format("Color.FromRgb({0}, {1}, {2})", r, g, b);
		}
		else if (prop.value_type.name == "Color" && str.StartsWith("#") && str.Length == 5)
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			int a, r, g, b;
			a = Convert.ToByte(str.Substring(1, 1), 16) * 17;
			r = Convert.ToByte(str.Substring(2, 1), 16) * 17;
			g = Convert.ToByte(str.Substring(3, 1), 16) * 17;
			b = Convert.ToByte(str.Substring(4, 1), 16) * 17;
			value_expression = String.Format("Color.FromArgb({0}, {1}, {2}, {3})", a, r, g, b);
		}
		else if (prop.value_type.name == "Color" && str.StartsWith("#") && str.Length == 4)
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			int r, g, b;
			r = Convert.ToByte(str.Substring(1, 1), 16) * 17;
			g = Convert.ToByte(str.Substring(2, 1), 16) * 17;
			b = Convert.ToByte(str.Substring(3, 1), 16) * 17;
			value_expression = String.Format("Color.FromRgb({0}, {1}, {2})", r, g, b);
		}
		else if (prop.value_type.name == "Color" && str.ToLowerInvariant() == "transparent")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = "Colors.Transparent";
		}
		else if (prop.value_type.name == "Color" && str.ToLowerInvariant() == "white")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = "Colors.White";
		}
		else if (prop.value_type.name == "Color")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = String.Format("Colors.{0}", str);
		}
		else if (prop.value_type.name == "Brush" && str.StartsWith("#"))
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			var color_expression = attribute_string_to_expression(element,
				types["SolidColorBrush"].props["Color"], str);
			value_expression = String.Format("new SolidColorBrush({0})", color_expression);
		}
		else if (prop.value_type.name == "Brush" && str.ToLowerInvariant() == "transparent")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = "Brushes.Transparent";
		}
		else if (prop.value_type.name == "Brush" && str.ToLowerInvariant() == "white")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = "Brushes.White";
		}
		else if (prop.value_type.name == "Brush")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = String.Format("Brushes.{0}", str);
		}
		else if (prop.value_type.name == "FontStyle" && str == "Italic")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = String.Format("FontStyles.{0}", str);
		}
		else if (prop.value_type.name == "FontWeight" && str.ToLowerInvariant() == "bold")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = "FontWeights.Bold";
		}
		else if (prop.value_type.name == "FontWeight")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			int val;
			if (int.TryParse(str, out val))
			{
				value_expression = String.Format("FontWeight.FromOpenTypeWeight({0})", val);
			}
			else
			{
				value_expression = String.Format("FontWeights.{0}", str);
			}
		}
		else if (prop.value_type.name == "Geometry")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = String.Format("Geometry.Parse({0})", quote_string(str));
		}
		else if (prop.value_type.name == "Point" && str.Contains(","))
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = String.Format("new Point({0})", str);
		}
		else if (prop.value_type.name == "Thickness")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			string[] values = str.Split(',');
			if (values.Length == 1)
			{
				value_expression = String.Format("new Thickness({0})", str);
			}
			else if (values.Length == 2)
			{
				value_expression = String.Format("new Thickness({0},{0})", str);
			}
			else if (values.Length == 4)
			{
				value_expression = String.Format("new Thickness({0})", str);
			}
			else
			{
				throw new NotImplementedException(String.Format("cannot convert Thickness value {0}", str));
			}
		}
		else if (prop.value_type.name == "CornerRadius")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = String.Format("new CornerRadius({0})", str);
		}
		else if (prop.value_type.name == "PropertyPath")
		{
			if (prop.value_type.ns != null)
				namespaces.Add(prop.value_type.ns);
			value_expression = String.Format("new PropertyPath({0})", quote_string(str));
		}
		else if (prop.value_type.name == "RoutedEvent")
		{
			value_expression = String.Format("{0}Event", str);
		}
		else if (prop.value_type.name == "double" && str.EndsWith("px"))
		{
			var num = str.Substring(0, str.Length - 2);
			Double.Parse(num);
			value_expression = num;
		}
		else if (prop.value_type.name == "double" && str.EndsWith("pt"))
		{
			var num = str.Substring(0, str.Length - 2);
			Double.Parse(num);
			value_expression = (Double.Parse(num) * 96 / 72).ToString();
		}
		else if (prop.value_type.name == "double")
		{
			Double.Parse(str);
			value_expression = str;
		}
		else if (prop.value_type.name == "int")
		{
			int.Parse(str);
			value_expression = str;
		}
		else if (prop.value_type.name == "object" || prop.value_type.name == "string")
		{
			value_expression = String.Format("{0}", quote_string(str));
		}
		else if (prop.value_type.name == "event")
		{
			value_expression = str;
		}
		else if (prop.value_type.name == "Duration" && str.Contains(":"))
		{
			string[] parts = str.Split(':');
			int days, hours, minutes, seconds, ms;
			if (parts[0].Contains("."))
			{
				string[] parts2 = parts[0].Split('.');
				days = int.Parse(parts2[0]);
				hours = int.Parse(parts2[1]);
			}
			else
			{
				days = 0;
				hours = int.Parse(parts[0]);
			}
			minutes = int.Parse(parts[1]);
			if (parts.Length > 2)
			{
				if (parts[2].Contains("."))
				{
					string[] parts2 = parts[2].Split('.');
					seconds = int.Parse(parts2[0]);
					ms = int.Parse(parts2[1] + new String('0', 3 - parts2[1].Length));
				}
				else
				{
					seconds = int.Parse(parts[2]);
					ms = 0;
				}
			}
			else
			{
				seconds = 0;
				ms = 0;
			}
			namespaces.Add("System"); // for TimeSpan
			value_expression = String.Format("new Duration(new TimeSpan({0},{1},{2},{3},{4}))",
				days, hours, minutes, seconds, ms);
		}
		else if (prop.value_type.name == "GridLength")
		{
			if (str == "Auto")
			{
				value_expression = "GridLength.Auto";
			}
			else if (str == "*")
			{
				value_expression = "new GridLength(1, GridUnitType.Star)";
			}
			else if (str.EndsWith("*"))
			{
				var num = str.Substring(0, str.Length - 1);
				Double.Parse(num);
				value_expression = String.Format("new GridLength({0}, GridUnitType.Star)", num);
			}
			else
			{
				Double.Parse(str);
				value_expression = str;
			}
		}
		else if (prop.value_type.name == "FontFamily")
		{
			namespaces.Add("System.Windows.Media");
			value_expression = String.Format("new FontFamily({0})", quote_string(str));
		}
		else if (prop.value_type.name == "Uri")
		{
			namespaces.Add("System");
			value_expression = String.Format("new Uri({0})", quote_string(str));
		}
		else
		{
			throw new Exception(String.Format("failed converting {1} property value {0}", str, prop.value_type.name));
		}

		return value_expression;
	}

	public void ReadXaml(string filename, string[] references)
	{
		XamlElement current = null;
		using (var reader = XmlReader.Create(filename))
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					var parent = current;
					current = new XamlElement();
					current.parent = parent;
					string local_name;
					bool empty = reader.IsEmptyElement;
					if (root_element == null)
					{
						root_element = current;
					}
					if (parent != null)
						current.target_type = parent.target_type;
					if (reader.Name.Contains("."))
					{
						int index = reader.Name.LastIndexOf(".");
						string container_typename = reader.Name.Substring(0, index);
						string container_propname = reader.Name.Substring(index+1);
						XamlProperty prop = null;
						XamlType container_type = null;
						if (!types.TryGetValue(container_typename, out container_type))
							throw new NotImplementedException(String.Format("type {0}", container_typename));
						if (!container_type.LookupProp(container_propname, out prop))
							throw new NotImplementedException(String.Format("property {0}", container_propname));
						container_type = prop.container_type;

						if (prop == null)
							throw new NotImplementedException(String.Format("element {0}", reader.Name));

						current.type = prop.value_type;
						current.prop = prop;

						if (reader.GetAttribute("x:Name") != null &&
							!elements_by_local.ContainsKey(reader.GetAttribute("x:Name")))
						{
							local_name = reader.GetAttribute("x:Name");
						}
						else if (reader.GetAttribute("x:Uid") != null &&
							!elements_by_local.ContainsKey(reader.GetAttribute("x:Uid")))
						{
							local_name = reader.GetAttribute("x:Uid");
						}
						else
						{
							int i = 0;
							do
							{
								local_name = String.Format("{0}{1}", current.type.name.ToLower(), i);
								i++;
							} while (elements_by_local.ContainsKey(local_name));
						}

						if (prop.auto)
						{
							current.early_init.Add(String.Format("{0} {1} = {2}.{3};", current.type.name, local_name, parent.local_name, prop.name));
						}
						else
						{
							current.early_init.Add(String.Format("{0} {1} = new {2}();", current.type.name, local_name, prop.value_type.name));
							current.late_init.Add(String.Format("{0}.{1} = {2};", parent.local_name, prop.name, local_name));
						}
					}
					else if (!types.TryGetValue(reader.Name, out current.type))
					{
						throw new NotImplementedException(String.Format("type {0}", reader.Name));
					}
					else
					{
						current.is_template = (parent != null &&
							(parent.is_template || parent.type.is_template));

						bool needs_declaration = true;
						bool needs_creation = true;
						if (current == root_element) {
							local_name = "this";
							needs_declaration = false;
							needs_creation = false;
						}
						else if ((reader.GetAttribute("x:Name") != null &&
							!elements_by_local.ContainsKey(reader.GetAttribute("x:Name")))
							|| (reader.GetAttribute("Name") != null &&
							!elements_by_local.ContainsKey(reader.GetAttribute("Name"))))
						{
							local_name = reader.GetAttribute("x:Name");
							if (local_name == null)
								local_name = reader.GetAttribute("Name");
							if (current.is_template)
							{
								current.class_decls.Add(String.Format("public FrameworkElementFactory {0};",
									local_name));
							}
							else
							{
								current.class_decls.Add(String.Format("public {0} {1};",
									current.type.name, local_name));
							}
							needs_declaration = false;
						}
						else if (reader.GetAttribute("x:Uid") != null &&
							!elements_by_local.ContainsKey(reader.GetAttribute("x:Uid")))
						{
							local_name = reader.GetAttribute("x:Uid");
						}
						else
						{
							int i = 0;
							do
							{
								local_name = String.Format("{0}{1}", current.type.name.ToLower(), i);
								i++;
							} while (elements_by_local.ContainsKey(local_name));
						}
						if (needs_declaration) {
							if (current.is_template)
							{
								current.early_init.Add(String.Format("FrameworkElementFactory {0};", local_name));
							}
							else
								current.early_init.Add(String.Format("{0} {1};", current.type.name, local_name));
						}
						if (needs_creation) {
							if (current.is_template)
							{
								if (reader.GetAttribute("x:Name") != null)
									current.early_init.Add(String.Format("{1} = new FrameworkElementFactory(typeof({0}), {3});", current.type.name, local_name, reader["x:Name"], quote_string(local_name)));
								else
									current.early_init.Add(String.Format("{1} = new FrameworkElementFactory(typeof({0}));", current.type.name, local_name));
							}
							else
								current.early_init.Add(String.Format("{1} = new {0}();", current.type.name, local_name));
						}
						if (current.parent != null &&
							!current.parent.has_attributes &&
							current.parent.prop != null &&
							current.type.SubclassOf(current.parent.prop.value_type))
						{
							current.prop = current.parent.prop;
							current.parent.early_init.Clear();
							current.parent.late_init.Clear();
							if (current.parent.prop.dependency)
							{
								if (current.parent.prop.container_type.ns != null)
									namespaces.Add(current.parent.prop.container_type.ns);
								current.parent.late_init.Add(String.Format("{0}.SetValue({1}.{2}Property, {3});", current.parent.parent.local_name, current.parent.prop.container_type.name, current.parent.prop.name, local_name));
							}
							else
							{
								current.early_init.Add(String.Format("{0}.{1} = {2};", current.parent.parent.local_name, current.parent.prop.name, local_name));
							}
						}
					}
					if (current.type.ns != null)
					{
						namespaces.Add(current.type.ns);
					}
					current.local_name = local_name;
					elements_by_local[local_name] = current;
					if (current.parent != null)
					{
						current.parent.children.Add(current);
					}
					if (reader.MoveToFirstAttribute())
					{
						current.has_attributes = true;
						do {
							bool handled_attribute = false;
							switch (reader.Name)
							{
							case "xmlns":
							case "xmlns:x":
							case "xmlns:ui":
							case "xml:lang":
							case "x:Uid":
							case "xmlns:MappingPIGen1":
								// Ignore
								handled_attribute = true;
								break;
							case "x:Class":
								current.class_name = reader.Value;
								handled_attribute = true;
								break;
							case "x:ClassModifier":
								current.class_modifier = reader.Value;
								handled_attribute = true;
								break;
							case "x:Name":
								current.name = reader.Value;
								XamlProperty name_prop;
								if (current.type.LookupProp("Name", out name_prop))
								{
									current.early_init.Add(String.Format("{0}.Name = {1};", current.local_name,
										attribute_string_to_expression(current, name_prop, reader.Value)));
								}
								elements_by_name[reader.Value] = current;
								handled_attribute = true;
								break;
							case "x:Key":
								current.key = reader.Value;
								XamlProperty key_prop;
								types["object"].LookupProp("_key", out key_prop);
								current.key_expr = attribute_string_to_expression(current, key_prop, reader.Value);
								static_resources.Add(current.key, current);
								handled_attribute = true;
								break;
							}
							if (!handled_attribute)
							{
								XamlProperty prop = null;
								if (reader.Name.Contains("."))
								{
									var index = reader.Name.LastIndexOf(".");
									var typename = reader.Name.Substring(0, index);
									XamlType container_type;
									if (types.TryGetValue(typename, out container_type))
									{
										var attrname = reader.Name.Substring(index+1);
										container_type.LookupProp(attrname, out prop);
									}
								}
								else
								{
									current.type.LookupProp(reader.Name, out prop);
								}
								if (prop != null)
								{
									string value_expression = attribute_string_to_expression(current, prop, reader.Value);
									if (prop.dependency)
									{
										if (prop.container_type.ns != null)
											namespaces.Add(prop.container_type.ns);
										current.early_init.Add(String.Format("{0}.SetValue({1}.{2}Property, {3});", current.local_name, prop.container_type.name, prop.name, value_expression));
									}
									else if (prop.value_type.name == "event")
									{
										current.early_init.Add(String.Format("{0}.{1} += {2};", current.local_name, prop.name, value_expression));
									}
									else
									{
										current.early_init.Add(String.Format("{0}.{1} = {2};", current.local_name, prop.name, value_expression));
									}
									handled_attribute = true;
								}
							}
							if (!handled_attribute)
							{
								throw new NotImplementedException(String.Format("attribute {0}", reader.Name));
							}
						} while (reader.MoveToNextAttribute());
					}
					if (current.parent != null &&
						current.prop == null)
					{
						if (current.parent.is_template)
						{
							current.late_init.Add(String.Format("{0}.AppendChild({1});",
								current.parent.local_name, current.local_name));
						}
						else if (current.key != null)
						{
							current.late_init.Add(
								current.parent.type.AddWithKeyStatement(
									current.parent.local_name,
									current.key_expr,
									current.local_name));
						}
						else
						{
							current.late_init.Add(
								current.parent.type.AddStatement(
									current,
									current.parent.local_name,
									current.local_name));
						}
					}
					if (empty)
					{
						current = current.parent;
					}
				}
				else if (reader.NodeType == XmlNodeType.EndElement)
				{
					current = current.parent;
				}
				else if (reader.NodeType == XmlNodeType.Text)
				{
					XamlProperty prop;
					XamlElement element;
					if (!current.has_attributes && current.prop != null)
					{
						prop = current.prop;
						element = current.parent;
						current.early_init.Clear();
						current.late_init.Clear();
					}
					else
					{
						prop = current.type.ContentProp;
						element = current;
					}
					if (prop.value_type.IsCollection)
					{
						string value_expression = attribute_string_to_expression(current, types["object"].props["_textcontent"], reader.Value.Trim());
						element.early_init.Add(
							prop.value_type.AddStatement(
								element,
								String.Format("{0}.{1}", element.local_name, prop.name),
								value_expression));
					}
					else
					{
						string value_expression = attribute_string_to_expression(current, prop, reader.Value.Trim());
						if (prop.dependency)
						{
							if (prop.container_type.ns != null)
								namespaces.Add(prop.container_type.ns);
							element.early_init.Add(String.Format("{0}.SetValue({1}.{2}Property, {3});", element.local_name, prop.container_type.name, prop.name, value_expression));
						}
						else
						{
							element.early_init.Add(String.Format("{0}.{1} = {2};", element.local_name, prop.name, value_expression));
						}
					}
				}
			}
		}
	}

	private void WriteDecls(TextWriter f, XamlElement element)
	{
		foreach (var line in element.class_decls)
		{
			f.WriteLine(line);
		}
		foreach (var child in element.children)
		{
			WriteDecls(f, child);
		}
	}

	private void WriteInitializeComponent(TextWriter f, XamlElement element)
	{
		foreach (var line in element.early_init)
		{
			f.WriteLine(line);
		}
		foreach (var child in element.children)
		{
			WriteInitializeComponent(f, child);
		}
		foreach (var line in element.late_init)
		{
			f.WriteLine(line);
		}
	}

	public void WriteCs(TextWriter f)
	{
		f.WriteLine("// This file was generated by xaml2cs");
		f.WriteLine();

		string ns = null;
		string class_name = null;

		int i = root_element.class_name.LastIndexOf(".");
		ns = root_element.class_name.Substring(0, i);
		class_name = root_element.class_name.Substring(i+1);

		foreach (var used_ns in namespaces)
		{
			f.WriteLine("using {0};", used_ns);
		}

		f.WriteLine("namespace {0} {{", ns);
		f.WriteLine("{1} partial class {0} : {2} {{", class_name, root_element.class_modifier, root_element.type.name);
		WriteDecls(f, root_element);
		f.WriteLine("public void InitializeComponent() {");
		WriteInitializeComponent(f, root_element);
		f.WriteLine("}"); // end InitializeComponent
		f.WriteLine("}"); // end class
		f.WriteLine("}"); // end namespace
	}

	public static void Main(string[] arguments)
	{
		var instance = new Xaml2Cs();

		var filename = arguments[0];
		var references = new string[0]; // FIXME

		instance.ReadXaml(filename, references);
		instance.WriteCs(System.Console.Out);
	}
}
