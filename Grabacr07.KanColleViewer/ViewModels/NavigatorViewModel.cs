﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Models;
using Grabacr07.KanColleViewer.Views.Controls;
using Livet;
using Livet.Commands;

namespace Grabacr07.KanColleViewer.ViewModels
{
	public class NavigatorViewModel : ViewModel, INavigator
	{
		#region Source 変更通知プロパティ

		private Uri _Source;

		public Uri Source
		{
			get { return this._Source; }
			set
			{
				this._Source = value;
				this.SourceString = value.ToString();
				this.RaisePropertyChanged();
			}
		}

		#endregion

		#region SourceString 変更通知プロパティ

		private string _SourceString;

		public string SourceString
		{
			get { return this._SourceString; }
			set
			{
				if (this._SourceString != value)
				{
					this._SourceString = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		#region IsNavigating 変更通知プロパティ

		private bool _IsNavigating;

		public bool IsNavigating
		{
			get { return this._IsNavigating; }
			set
			{
				if (this._IsNavigating != value)
				{
					this._IsNavigating = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		#region CanGoBack 変更通知プロパティ

		private bool _CanGoBack;

		public bool CanGoBack
		{
			get { return this._CanGoBack; }
			set
			{
				if (this._CanGoBack != value)
				{
					this._CanGoBack = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		#region CanGoForward 変更通知プロパティ

		private bool _CanGoForward;

		public bool CanGoForward
		{
			get { return this._CanGoForward; }
			set
			{
				if (this._CanGoForward != value)
				{
					this._CanGoForward = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		#region NavigateCommand コマンド

		private ViewModelCommand _NavigateCommand;

		public ViewModelCommand NavigateCommand
		{
			get
			{
				if (this._NavigateCommand == null)
				{
					this._NavigateCommand = new ViewModelCommand(this.Navigate);
				}
				return this._NavigateCommand;
			}
		}

		#endregion

		public event EventHandler GoBackRequested;
		public event EventHandler GoForwardRequested;
		public event EventHandler RefreshRequested;
		public event EventHandler<Uri> UriRequested;

		public void GoBack()
		{
			if (this.GoBackRequested != null) this.GoBackRequested(this, new EventArgs());
		}

		public void GoForward()
		{
			if (this.GoForwardRequested != null) this.GoForwardRequested(this, new EventArgs());
		}

		public void Refresh()
		{
			if (this.UriRequested != null) this.RefreshRequested(this, new EventArgs());
		}

		public void ReNavigate()
		{
			if (this.UriRequested != null) this.UriRequested(this, this.Source);
		}

		public void Navigate()
		{
			Uri uri;
			if (this.UriRequested != null && Uri.TryCreate(this.SourceString, UriKind.Absolute, out uri))
			{
				this.UriRequested(this, uri);
			}
		}
		public void CookieNavigate()
		{
			Uri uri;
			string CookieInject = "javascript:void(eval(\"document.cookie = 'ckcy=1;expires=Sun, 09 Feb " + DateTime.Now.AddYears(4).ToString("yyyy") + " 09:00:09 GMT;domain=osapi.dmm.com;path=/';document.cookie = 'ckcy=1;expires=Sun, 09 Feb " + DateTime.Now.AddYears(4).ToString("yyyy") + " 09:00:09 GMT;domain=203.104.209.7;path=/';document.cookie = 'ckcy=1;expires=Sun, 09 Feb " + DateTime.Now.AddYears(4).ToString("yyyy") + " 09:00:09 GMT;domain=www.dmm.com;path=/netgame/';\"));location.href=\"";
			CookieInject += this.SourceString + "\";";
			if (this.UriRequested != null && Uri.TryCreate(CookieInject, UriKind.Absolute, out uri))
			{
				this.UriRequested(this, uri);
			}
		}
	}
}
