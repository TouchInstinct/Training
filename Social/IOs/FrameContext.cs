using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace Touch.Common
{
	public class FrameContext
	{
		public UIView View;
		public RectangleF Frame;

		public RectangleF? ParentBounds;
		public RectangleF? RelativeFrame;

		public FrameContext(UIView view, UIView relativeView)
		{
			View = view;

			Frame = View.Frame;
			ParentBounds = View.Superview != null ? View.Superview.Bounds : (RectangleF?)null;
			RelativeFrame = relativeView != null ? relativeView.Frame : (RectangleF?)null;
		}

		public void Commit()
		{
			View.Frame = Frame;
		}
	}

	public static class FrameHelper
	{
		/// <summary>
		/// Get FrameContext
		/// </summary>
		public static FrameContext Begin(this UIView view, UIView relativeView = null)
		{
			FrameContext fc = new FrameContext(view, relativeView);
			return fc;
		}

		#region Coordinates and dimensions
		public static FrameContext X(this FrameContext fc, float x)
		{
			fc.Frame.X = x;
			return fc;
		}

		public static FrameContext Y(this FrameContext fc, float y)
		{
			fc.Frame.Y = y;
			return fc;
		}

		public static FrameContext Width(this FrameContext fc, float width)
		{
			fc.Frame.Width = width;
			return fc;
		}

		public static FrameContext Height(this FrameContext fc, float height)
		{
			fc.Frame.Height = height;
			return fc;
		}
		#endregion

		#region Alignment
		public static FrameContext AlignLeft(this FrameContext fc, float dx = 0f)
		{
			fc.Frame.X = dx;
			return fc;
		}

		public static FrameContext AlignLeft(this FrameContext fc, UIView relativeView, float dx = 0f)
		{
			fc.Frame.X = relativeView.Frame.X + dx;
			return fc;
		}

		public static FrameContext AlignTop(this FrameContext fc, float topMargin = 0f)
		{
			fc.Frame.Y = topMargin;
			return fc;
		}

		public static FrameContext AlignTop(this FrameContext fc, UIView relativeView, float topMargin = 0f)
		{
			return fc.AlignTop(relativeView.Frame, topMargin);
		}

		public static FrameContext AlignTop(this FrameContext fc, RectangleF relativeViewFrame, float topMargin = 0f)
		{
			fc.Frame.Y = relativeViewFrame.Y + topMargin;
			return fc;
		}

		public static FrameContext AlignRight(this FrameContext fc, float rightMargin = 0f)
		{
			fc.Frame.X = fc.ParentBounds.Value.Width - fc.Frame.Width - rightMargin;
			return fc;
		}

		public static FrameContext AlignRight(this FrameContext fc, UIView relativeView, float rightMargin = 0f)
		{
			fc.Frame.X = relativeView.Frame.Right - fc.Frame.Width - rightMargin;
			return fc;
		}

		public static FrameContext AlignBottom(this FrameContext fc)
		{
			return fc.BMargin(0f);
		}

		public static FrameContext AlignBottom(this FrameContext fc, UIView relativeView, float bottomMargin = 0f)
		{
			fc.Frame.Y = relativeView.Frame.Bottom - fc.Frame.Height - bottomMargin;
			return fc;
		}
		#endregion

		#region Margin
		public static FrameContext BMargin(this FrameContext fc, float bottomMargin)
		{
			fc.Frame.Y = fc.ParentBounds.Value.Height - fc.Frame.Height - bottomMargin;
			return fc;
		}

		public static FrameContext LMargin(this FrameContext fc, float leftMargin)
		{
			fc.Frame.X = leftMargin;
			return fc;
		}

		public static FrameContext TMagrin(this FrameContext fc, float topMargin)
		{
			fc.Frame.Y = topMargin;
			return fc;
		}
		#endregion

		#region Placement
		public static FrameContext PlaceAbove(this FrameContext fc, UIView viewBelow, float dy = 0f)
		{
			fc.Frame.Y = viewBelow.Frame.Y - fc.Frame.Height + dy;
			return fc;
		}

		public static FrameContext PlaceBelow(this FrameContext fc, float dy = 0f)
		{
			fc.Frame.Y = fc.ParentBounds.Value.Height + dy;
			return fc;
		}

		public static FrameContext PlaceBelow(this FrameContext fc, UIView viewAbove, float dy = 0f)
		{
			fc.Frame.Y = viewAbove.Frame.Bottom + dy;
			return fc;
		}

		public static FrameContext PlaceRight(this FrameContext fc, UIView pivot, float dx = 0f)
		{
			fc.Frame.X = pivot.Frame.Right + dx;
			return fc;
		}

		public static FrameContext CenterH(this FrameContext fc)
		{
			fc.Frame.X = (fc.ParentBounds.Value.Width - fc.Frame.Width) / 2;
			return fc;
		}

		public static FrameContext CenterV(this FrameContext fc)
		{
			fc.Frame.Y = (fc.ParentBounds.Value.Height - fc.Frame.Height) / 2;
			return fc;
		}

		/// <summary>
		/// Расплолагает view левее центра родителя. Есть возможность сдвига на dx
		/// </summary>
		public static FrameContext LeftOfCenter(this FrameContext fc, float dx = 0f)
		{
			fc.Frame.X = fc.ParentBounds.Value.Width / 2 + dx;
			return fc;
		}
		#endregion

		#region Filling
		public static FrameContext FillHorizontally(this FrameContext fc, float left = 0f, float right = 0f)
		{
			fc.Frame.X = left;
			fc.Frame.Width = fc.ParentBounds.Value.Width - left - right;
			return fc;
		}

		public static FrameContext FillBelow(this FrameContext fc)
		{
			float height = fc.ParentBounds.Value.Height - fc.Frame.Top;

			fc.Frame.Height = height;
			return fc;
		}
		#endregion

		#region Movement
		public static FrameContext MoveY(this FrameContext fc, float dy)
		{
			fc.Frame.Y += dy;
			return fc;
		}
		#endregion
	}
}

