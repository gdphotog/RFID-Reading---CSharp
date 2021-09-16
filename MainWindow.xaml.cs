using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace RFIDReading_2
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    //Setup variables for reading
    string strRFIDHEXRaw;
    string strRFIDHEXCorrected;
    long lngFolio16Digit;

    public MainWindow()
    {
      InitializeComponent();
      

    }

    private void txtRFIDInput_keydown(object sender, KeyEventArgs e)
    {
      // RFID Card has a Carriage Return (Enter) at the end of it, so we look for the Enter Keypress      
      if (e.Key == Key.Return)
      {
        Trace.WriteLine("Value accepted");
        
        //Save HEX info to Raw String variable
        strRFIDHEXRaw = txtRFIDInput.Text.ToString();

        //Clear the Text box 
        txtRFIDInput.Clear();

        // Set the Raw HEX to the label 
        LblRawHEX.Content = strRFIDHEXRaw;

        //Call function to flip the RFID HEX.
        rfidconvert();

        //Store flipped HEX into the label
        LblDecodedHEX.Content = strRFIDHEXCorrected;

        //Call function to convert Flipped HEX to the 16 digit folio number
        hextolong();

        //display Folio number
        Lbl16DigitFolio.Content = lngFolio16Digit;


        //Reset variables
        strRFIDHEXCorrected = "";
        strRFIDHEXRaw = "";
        lngFolio16Digit = 0;
      }

    }

    public void rfidconvert()
    {

      // DO...WHILE loop to flip the HEX pairs while there is data in the RAW HEX.
      do {
        //Take last 2 characters and put them into the Corrected String
        strRFIDHEXCorrected = strRFIDHEXCorrected + strRFIDHEXRaw.Substring(strRFIDHEXRaw.Length - 2, 2);

        //Remove the 2 Characters just read from the RAW HEX field
        strRFIDHEXRaw = strRFIDHEXRaw.Trim().Remove(strRFIDHEXRaw.Length - 2);
        Trace.WriteLine("Corrected HEX: {0}",strRFIDHEXCorrected);
        Trace.WriteLine("Raw HEX: {0}",strRFIDHEXRaw);
      
      } while (strRFIDHEXRaw.Length >0 );
      
    }

    public void hextolong()
    {
      //Conversion from Hex to Long integer
      lngFolio16Digit = Convert.ToInt64(strRFIDHEXCorrected, 16);


    }
  }
}
