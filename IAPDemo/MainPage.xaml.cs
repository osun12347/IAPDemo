using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IAPDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Init();
        }

        private async void Init()
        {
            StorageFile proxyFile = await Package.Current.InstalledLocation.GetFileAsync("in-app-purchase.xml");
            await CurrentAppSimulator.ReloadSimulatorAsync(proxyFile);
        }



        private void Check2(object sender, RoutedEventArgs e)
        {
            LicenseInformation licenseInformation = CurrentAppSimulator.LicenseInformation;
            var productLicense = licenseInformation.ProductLicenses["product2"];
            if (productLicense.IsActive)
            {
                tb2.Text = "you can use Product2";
                //rootPage.NotifyUser("You can use " + productName + ".", NotifyType.StatusMessage);
            }
            else
            {
                //rootPage.NotifyUser("You don't own " + productName + ".", NotifyType.ErrorMessage);
                tb2.Text = "You dont own Product2";
            }
        }


        private async void Buy2(object sender, RoutedEventArgs e)
        {
            LicenseInformation licenseInformation = CurrentAppSimulator.LicenseInformation;
            if (!licenseInformation.ProductLicenses["product2"].IsActive)
            {

                try
                {
                    await CurrentAppSimulator.RequestProductPurchaseAsync("product2");
                    if (licenseInformation.ProductLicenses["product2"].IsActive)
                    {
                        //rootPage.NotifyUser("You bought " + productName + ".", NotifyType.StatusMessage);
                        tb2.Text = "You bought Product2";
                    }
                    else
                    {
                        //rootPage.NotifyUser(productName + " was not purchased.", NotifyType.StatusMessage);
                        tb2.Text = "Product2 was not purchased";
                    }
                }
                catch (Exception)
                {
                    //rootPage.NotifyUser("Unable to buy " + productName + ".", NotifyType.ErrorMessage);
                }
            }
            else
            {
                //rootPage.NotifyUser("You already own " + productName + ".", NotifyType.ErrorMessage);
            }
        }
    }
}
