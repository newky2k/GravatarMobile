using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GravatarMobile.Core.Interfaces;
using GravatarMobile.Core;
using GravatarMobile.Core.Data;

namespace GravatarMobile.WP
{
    public partial class GravatarView : UserControl, IGravatarView
    {
        #region Fields

        private Gravatar mItem;
        private Enums.GravatarViewStyle mStyle = Enums.GravatarViewStyle.Square;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Gravatar item.
        /// </summary>
        /// <value>The item.</value>
        public Gravatar Avatar
        {
            get
            {
                return mItem;
            }
            set
            {
                if (mItem != value)
                {
                    mItem = value;

                    LoadImage();
                }
            }
        }

        /// <summary>
        /// Gets or sets the view style.
        /// </summary>
        /// <value>The view style.</value>
        public Enums.GravatarViewStyle ViewStyle
        {
            get
            {
                return mStyle;
            }
            set
            {
                if (mStyle != value)
                {
                    mStyle = value;
                }
            }
        }

        #endregion


        public GravatarView()
        {
            InitializeComponent();
        }

        private async void LoadImage()
        {
            var size = (this.Width > 80) ? this.Width : 80;

            await mItem.LoadImage((int)size);

            this.Dispatcher.BeginInvoke(() =>
                {
                    imgAvatar.Source = mItem.ImageAsBitmap();
                   // mImageView.Image = mItem.ImageAsUIImage();

                });

        }
    }
}
