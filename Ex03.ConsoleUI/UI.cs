using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
namespace Ex03.ConsoleUI
{
    public class UI
    {
        Garage m_Garage;
        public UI()
        {
            m_Garage = new Garage();
        }

        public void Run()
       {
            
            int userChoice = 0;
            bool isValidUserChoice = false;
            printWelcomeMessage();

            while (true)
            {
                printProgramMenuOptions();

                while (!isValidUserChoice)
                {
                    getMenuChoice(ref userChoice, ref isValidUserChoice, 1, 8);
                }

                switch (userChoice)
                {
                    case 1:
                        addVehicleToGarage();
                        break;
                    case 2:
                        showAllVehiclesInGarage();
                        break;
                    case 3:
                        changeVehicleStatus();
                        break;
                    case 4:
                        inflateVehicleToMax();
                        break;
                    case 5:
                        refuelVehicle();
                        break;
                    case 6:
                        rechargeVehicle();
                        break;
                    case 7:
                        printVehicleInfo();
                        break;
                    case 8:
                        break;
                    default:
                        userChoice = -1;
                        break;
                }

                if (exitCheck(userChoice))
                {
                    break;
                }

                isValidUserChoice = false;
            }

            printGoodBye();
        }

        private void changeVehicleStatus()
        {
            string licenseInput = getLicenceNumber();
            eVehicleStatus newStatus = getVehicleStatus();

            try
            {
                m_Garage.ChangeVehicleStatus(licenseInput, newStatus);
                Console.WriteLine(string.Format(@"
The Vehicle status of {0} updated.
", licenseInput));
            }
            catch(FormatException)
            {
                Console.WriteLine(string.Format(@"
This is the same vehicle status!
"));
            }
            catch (VehicleDoesNotInGarageException)
            {
                Console.WriteLine(string.Format(@"
Error: This License does not exist in this Garage!
"));
            }
            catch (Exception i_CurrentExeption)
            {
                Console.WriteLine(string.Format(@"Unknown error occured: 
{0}
", i_CurrentExeption.Message));
            }

        }

        private eVehicleStatus getVehicleStatus()
        {
            int userChoice = 0;
            bool isValidUserChoice = false;
            eVehicleStatus resStatue = eVehicleStatus.InRepair;
            printStatusMenu();

            while (!isValidUserChoice)
            {
                getMenuChoice(ref userChoice, ref isValidUserChoice, 1, 3);
            }

            if (userChoice == 1)
            {
                resStatue = eVehicleStatus.InRepair;
            }
            else if (userChoice == 2)
            {
                resStatue = eVehicleStatus.Repaired;
            }
            else if (userChoice == 3)
            {
                resStatue = eVehicleStatus.Paid;
            }

            return resStatue;
        }
        private void printStatusMenu()
        {
            Console.Write(string.Format(@"Please choose a status option for change:
1. In Repair
2. Repaired
3. Paid
Your Choose: "));

        }

        private void showAllVehiclesInGarage()
        {
            int userChoice = 0;
            bool isValidUserChoice = false, isWholeDictionary = false ;
            eVehicleStatus status = eVehicleStatus.InRepair;
            List<string> licenseList;
            StringBuilder printString = new StringBuilder();
            printVehicleListMenu();

            while (!isValidUserChoice)
            {
                getMenuChoice(ref userChoice, ref isValidUserChoice, 1, 4);
            }

            if (userChoice == 1)
            {
                isWholeDictionary = true;
            }
            else if (userChoice == 2)
            {
                status = eVehicleStatus.InRepair;
            }
            else if (userChoice == 3)
            {
                status = eVehicleStatus.Repaired;
            }
            else if (userChoice == 4)
            {
                status = eVehicleStatus.Paid;
            }
            licenseList = m_Garage.GetListLicenseByStatus(status, isWholeDictionary);


            if (licenseList.Count == 0)
            {
                printString.Append(string.Format(@"
Vehicle does not found. 
", userChoice));
            }
            else
            {
                printString.Append(string.Format(@"
Vehicles with this status in the garage: 
"));
                foreach (string license in licenseList)
                {
                    printString.Append(license + Environment.NewLine);
                }
            }

            Console.WriteLine(Environment.NewLine + printString);
        }

        private void printVehicleListMenu()
        {
            Console.Write(string.Format(@"plese choose an option:
1. Print all the vehicles in the garage.
2. Print only vehicles 'In Reapir'
3. Print only vehicles were 'Repaired'
4. Print only vehicles were 'Paid'
Your choice: "));
        }
        private bool exitCheck(int i_UserChoice)
        {
            return i_UserChoice == 8;
        }
        private void getMenuChoice(ref int o_userChoice, ref bool o_isValidUserChoice, short i_MinVal, short i_MaxVal)
        {
            try
            {
                o_isValidUserChoice = getUserChoiceFromProgramMenu(out o_userChoice, i_MinVal, i_MaxVal);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a number.");
            }
            catch (ValueOutOfRangeException)
            {
                Console.WriteLine(string.Format(@"Error: The number should be between {0} to {1}", i_MinVal, i_MaxVal));
            }
            catch (Exception i_CurrentExeption)
            {
                Console.WriteLine(string.Format(@"Unknown error occured: 
{0}", i_CurrentExeption.Message));
            }
        }

        private bool getUserChoiceFromProgramMenu(out int o_UserChoice, int i_MinValue, int i_MaxValue)
        {
            if (!int.TryParse(Console.ReadLine(), out o_UserChoice))
            {
                throw new FormatException();
            }

            if (o_UserChoice < i_MinValue || o_UserChoice > i_MaxValue)
            {
                throw new ValueOutOfRangeException(new Exception(), i_MinValue, i_MaxValue);
            }

            return true;
        }

        private void printGoodBye()
        {
            Console.Write("Bye Bye.. It was a pleasure to serve you.");
        }       

        private void printWelcomeMessage()
        {
            drawCar();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                
                // Prompt the user to enter text
                synthesizer.Rate = -2;    // Increase for faster speech, decrease for slower speech
                synthesizer.Volume = 100; // Adjust volume from 0 to 100
                string sentence = "Wellcome to Aviv & Amit Garage! We are happy to help you!";
                Console.WriteLine(sentence);

                // Speak the sentence
                synthesizer.Speak(sentence);

                // Display the sentence on the console
                
            }

            Console.ResetColor();
        }
        void printProgramMenuOptions()
        {
            Console.Write(string.Format(@"Please choose a service from this list:
1. Enter a Vehicle to the garage.
2. Show all vehicle numbers in the garage 
3. Change a Vehicle Status 
4. Inflate a Vehicle to maximum
5. Refuel a Vehicle
6. Recharge a Vehicle
7. Show a Vehicle details
8. Exit
Your choice: "));
        }

        private void addVehicleToGarage()
        {
            string licenceNumber = getLicenceNumber(), ownerName = getName(), phoneNumber = getOwnerPhoneNum();
            eVehicleStatus vehicleStatus = eVehicleStatus.InRepair;
            VehicleInfo currentVehicleInfo = new VehicleInfo();

            try
            {
                currentVehicleInfo = getVehicleInfoFromUser(licenceNumber);
                m_Garage.AddVehicleCard(licenceNumber, ownerName, phoneNumber, vehicleStatus, currentVehicleInfo);
                printAddSuccesfullyMessage();
            }
            catch (VehicleInGarageException ex)
            {
                Console.WriteLine(string.Format(@"Vehicle with license number {0} is already in the garage." , ex.LicenceNumber));
            }
        }

        private void printVehicleInfo()
        {
            
            string licenceNumber = getLicenceNumber();
            Vehicle vehicle= null;
            VehicleCard vehiclcard = null;
            try
            {
                vehicle= m_Garage.returnVehicle(licenceNumber,out vehiclcard);
                printVehicleByType(vehiclcard, vehicle, vehicle.GetType().ToString(), vehicle.Energy.GetType().ToString());
            }
            catch (VehicleDoesNotInGarageException ex)
            {
                Console.WriteLine(string.Format(@"There is no Vehicle with this  license number {0} in the garage.", ex.LicenceNumber));
            }

        }
        private void printVehicleByType(VehicleCard card ,Vehicle vehicle,string vehicleType, string EnergyType)
        {
            vehicleType= vehicleType.Substring("Ex03.GarageLogic.".Length);
            EnergyType = EnergyType.Substring("Ex03.GarageLogic.".Length);
            switch (vehicleType)
            {
                case "Car":
                    if(EnergyType == "Electricity")
                    {
                        printElectricCarInfo(card, vehicle);
                    }
                    else if (EnergyType == "Fuel")
                    {
                        printFuelCarInfo(card, vehicle);
                    }
                    break;

               case "Motorcycle":
                    if (EnergyType == "Electricity")
                    {
                        printElectricMotorcycleInfo(card, vehicle);
                    }
                    else if (EnergyType == "Fuel")
                    {
                        printFuelMotorcycleInfo(card, vehicle);
                    }
                    break;
                case "Truck":
                    printTruckInfo(card, vehicle);
                    break;
            }
            Console.WriteLine();
        }
        private void printElectricCarInfo(VehicleCard i_vehicleCard, Vehicle i_vehicle)
        {
            if (i_vehicle is Car electricCar)
            {

                printGenralVehicleData(i_vehicleCard, i_vehicle);
                Console.WriteLine($"Battery time left: {i_vehicle.Energy.CurrentEnergy} out of {electricCar.Energy.MaxEnergy}");
                Console.WriteLine($"Color: {electricCar.Color}");
                Console.WriteLine($"Number of Doors: {electricCar.Doors}");
                Console.WriteLine($"Fuel Type: Electricty");
                
                
            }
            else
            {
                Console.WriteLine("Invalid vehicle type. Expected a Car object.");
            }
        }
        private void printFuelCarInfo(VehicleCard i_vehicleCard, Vehicle i_vehicle)
        {
            if (i_vehicle is Car fuelCar)
            {

                printGenralVehicleData(i_vehicleCard, i_vehicle);
                Console.WriteLine($"Fuel left in the tank: {fuelCar.Energy.CurrentEnergy} liters out of {fuelCar.Energy.MaxEnergy} liters");
                Console.WriteLine($"Color: {fuelCar.Color}");
                Console.WriteLine($"Number of Doors: {fuelCar.Doors}");
                Console.WriteLine($"Fuel Type: {fuelCar.FuelType}");
                

            }
            else
            {
                Console.WriteLine("Invalid vehicle type. Expected a Car object.");
            }
        }

        private void printTruckInfo(VehicleCard i_vehicleCard, Vehicle i_vehicle)
        {
           
            if (i_vehicle is Truck truck)
            {
               string IsTruckContainsDangerousMaterials = truck.IsContainsDangerousMaterials ? "yes" : "no";
                printGenralVehicleData(i_vehicleCard, i_vehicle);
                Console.WriteLine($"Fuel Type: {truck.FuelType}");
                Console.WriteLine($"Fuel left in the tank: {truck.Energy.CurrentEnergy} liters out of {truck.Energy.MaxEnergy} liters");
                Console.WriteLine($"Is truck contains dangerous materials : {IsTruckContainsDangerousMaterials}");
            }
            else
            {
                Console.WriteLine("Invalid vehicle type. Expected a Truck object.");
            }
        }
        void printGenralVehicleData(VehicleCard i_vehicleCard, Vehicle i_vehicle)
        {
            Console.WriteLine("Car Information:");
            Console.WriteLine($"License Number: {i_vehicle.LicenceNumber}");
            Console.WriteLine($"Model Name: {i_vehicle.ModelName}");
            Console.WriteLine($"Owner Name: {i_vehicleCard.OwnerName}");
            Console.WriteLine($"VehicleStatus: {i_vehicleCard.VehicleStatus}");
            Console.WriteLine($"Owner phone Number: {i_vehicleCard.PhoneNumber}");
            Console.WriteLine($"Number of Wheels: {i_vehicle.Wheels.Count}");
            Console.WriteLine($"Air Pressure in Wheels is: {i_vehicle.Wheels[0].CurrentAirPressure} bars out of {i_vehicle.Wheels[0].MaxAirPressure} ");
            

        }

        void drawCar()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("          _________");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("         ////||||||\\");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("    ''''\\__|  -|  -|_____");
            Console.WriteLine("     (')|------------|(')");
            Console.WriteLine( );
            Console.ResetColor();
        }
        private void printElectricMotorcycleInfo(VehicleCard i_vehicleCard, Vehicle i_vehicle)
        {
            if (i_vehicle is Motorcycle electricmotorcycle)
            {

                printGenralVehicleData(i_vehicleCard, i_vehicle);
                Console.WriteLine($"Fuel Type: Electricty");
                Console.WriteLine($"Battery time left: {electricmotorcycle.Energy.CurrentEnergy} out of {electricmotorcycle.Energy.MaxEnergy}");
                Console.WriteLine($"Engine capacity : {electricmotorcycle.EngineCapacity}"); 
                Console.WriteLine($"Motorcicly Licence Type : {electricmotorcycle.MotorciclyLicenceType}");
            }
            else
            {
                Console.WriteLine("Invalid vehicle type. Expected a Motorcycle object.");
            }
        }
        private void printFuelMotorcycleInfo(VehicleCard i_vehicleCard, Vehicle i_vehicle)
        {
            if (i_vehicle is Motorcycle fuelMotorcycle)
            {

                printGenralVehicleData(i_vehicleCard, i_vehicle);
                Console.WriteLine($"Fuel Type:{fuelMotorcycle.FuelType} ");
                Console.WriteLine($"Fuel lett in the tank: {fuelMotorcycle.Energy.CurrentEnergy} out of {fuelMotorcycle.Energy.MaxEnergy}");
                Console.WriteLine($"Engine capacity : {fuelMotorcycle.EngineCapacity}");
                Console.WriteLine($"Motorcicly Licence Type : {fuelMotorcycle.MotorciclyLicenceType}");
            }
            else
            {
                Console.WriteLine("Invalid vehicle type. Expected a Motorcycle object.");
            }
        }
        
        private void printAddSuccesfullyMessage()
        {
            Console.WriteLine("The vehicle added successfuly to the garage");
        }

        private string getLicenceNumber() // could contain also letters
        {
            Console.Write("Plaese enter your Vehicle licence Number: ");
            return Console.ReadLine();
        }
        private string getName()
        {
            bool isValidName = false;
            string ownerName = string.Empty;

            while (!isValidName)
            {
                Console.Write("Plese enter your Name: ");
                ownerName = Console.ReadLine();

                try
                {
                    checkValidOwnerName(ownerName, out isValidName);
                }                
                catch (NullReferenceException)
                {
                    Console.WriteLine("Error: The name can't be empty");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: The name should contain only letters and spaces");
                }
                catch (Exception i_CurrentExeption)
                {
                    Console.WriteLine(string.Format(@"Unknown error occured: 
{0}", i_CurrentExeption.Message));
                }
            }

            return ownerName;
        }

        private void checkValidOwnerName(string i_OwnerName, out bool o_IsValidName)
        {
            if (string.IsNullOrEmpty(i_OwnerName))
            {
                throw new NullReferenceException();
            }

            foreach (char c in i_OwnerName)
            {
                bool isUpperCase = (c >= 'A' && c <= 'Z'), isUnderCase = (c >= 'a' && c <= 'z'), isSpaceChar = c == ' ';
                if (!isUpperCase && !isUnderCase && !isSpaceChar)
                {
                    throw new FormatException();
                }
            }
            o_IsValidName = true;
        }
        private string getOwnerPhoneNum()
        {
            string phoneNum = string.Empty;
           
            bool isValidPhoneNum = false;
            while (!isValidPhoneNum)
            {
                printGetPhoneNumber();
                phoneNum = Console.ReadLine();

                try
                {
                    checkValidPhoneNum(phoneNum, out isValidPhoneNum);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Error: The phone number can't be empty");
                }
                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Error: The phone number's length should be 10 digits");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Only digits allowed");
                }
                catch (Exception i_CurrentExeption)
                {
                    Console.WriteLine(string.Format(@"Unknown error occured: 
{0}", i_CurrentExeption.Message));
                }

            }

            return phoneNum;
        }

        private void checkValidPhoneNum(string i_PhoneNum, out bool o_IsValidPhoneNum)
        {
            o_IsValidPhoneNum = false;

            if (string.IsNullOrEmpty(i_PhoneNum))
            {
                throw new NullReferenceException();
            }
            else if (i_PhoneNum.Length != 10)
            {
                throw new ValueOutOfRangeException(new Exception(), 10, 10);
            }
            else if (false ==i_PhoneNum.All(char.IsDigit))
            {
                throw new FormatException();
            }
            else
            {
                o_IsValidPhoneNum = true;
            }
        }
        private void printGetPhoneNumber()
        {
            Console.Write("Please enter an owner's phone number: ");
        }

                    
        private VehicleInfo getVehicleInfoFromUser(string i_LicenseNumber)
        {
            VehicleInfo o_VehicleInfo = new VehicleInfo();
            
            o_VehicleInfo.m_LicenseNumber = i_LicenseNumber;
            getVehicleTypeFromUser(out o_VehicleInfo.m_VehicleType);
            getVehicleModelFromUser(out o_VehicleInfo.m_ModelName);
            getInfosByVehicleTypeFromUser(o_VehicleInfo.m_VehicleType, ref o_VehicleInfo);
            getWheelsInfoFromUser(o_VehicleInfo.m_VehicleType, ref o_VehicleInfo.m_WheelManufacturerName, ref o_VehicleInfo.m_CurrentAirPressure);
            return o_VehicleInfo;
        }

        private void getVehicleModelFromUser(out string i_ModelName)
        {
            Console.Write("plese enter vehicle model Name: ");
            i_ModelName = Console.ReadLine();
        }
        private void getWheelsInfoFromUser(eVehicleType i_VehicleType, ref string  ManufacturerName, ref float o_CurrentAirPressure)
        {
            float minAirPressure = 0;
            Console.Write("Enter wheels Manufacturer Name. ");
            ManufacturerName = getName();
            float maxAirPressure = getMaxAirPressure(i_VehicleType);
            Console.Write("Please enter current Air pressure: ");
            bool isValidInput = false;

            while (!isValidInput)
            {
                getCapacityFromUser(ref o_CurrentAirPressure, ref isValidInput, minAirPressure, maxAirPressure);
            }
        }
        private float getMaxAirPressure(eVehicleType i_VehicleType)
        {
            float MaxAirPressure=0;
            switch (i_VehicleType)
            {
                case eVehicleType.Truck:
                    MaxAirPressure = 26;
                    break;
                case eVehicleType.MotorcycleElectric:
                    MaxAirPressure = 31;
                    break;
                case eVehicleType.MotorcycleFuel:
                    MaxAirPressure = 31;
                    break;
                case eVehicleType.CarFuel:
                    MaxAirPressure = 33;
                    break;
                case eVehicleType.CarElectric:
                    MaxAirPressure = 33;
                    break;
            }

            return MaxAirPressure;
        }
        private void getInfosByVehicleTypeFromUser(eVehicleType i_VehicleType, ref VehicleInfo o_VehicleInfo)
        {
            switch(i_VehicleType)
            {
                case eVehicleType.Truck:
                    o_VehicleInfo.m_IsContainsDangerousMaterials = isTruckContainsDangerousMaterials();
                    o_VehicleInfo.m_CargoVolume = truckCargoVolume();
                    o_VehicleInfo.m_FuelType = getFuelTypeFromUser();
                    o_VehicleInfo.m_CurrentFuelCapacity = getCurrentFuelCapacityFromUser(i_VehicleType);
                    break;
                case eVehicleType.MotorcycleElectric:
                    o_VehicleInfo.m_LicenseType = getMotorcycleLicenceTypeFromUser();
                    o_VehicleInfo.m_EngineCapacity = getMotorcycleEnginevolumeFromUser();
                    o_VehicleInfo.m_RemainingBatteryTime = getRemainingBatteryTimeFromUser(i_VehicleType);
                    break;
                case eVehicleType.MotorcycleFuel:
                    o_VehicleInfo.m_LicenseType = getMotorcycleLicenceTypeFromUser();
                    o_VehicleInfo.m_EngineCapacity = getMotorcycleEnginevolumeFromUser();
                    o_VehicleInfo.m_FuelType = getFuelTypeFromUser();
                    o_VehicleInfo.m_CurrentFuelCapacity = getCurrentFuelCapacityFromUser(i_VehicleType);
                    break;
                case eVehicleType.CarFuel:
                    o_VehicleInfo.m_Color = getCarColor();
                    o_VehicleInfo.m_Doors = getCarNumberOfDoors();
                    o_VehicleInfo.m_FuelType = getFuelTypeFromUser();
                    o_VehicleInfo.m_CurrentFuelCapacity = getCurrentFuelCapacityFromUser(i_VehicleType);
                    break;
                case eVehicleType.CarElectric:
                    o_VehicleInfo.m_Color = getCarColor();
                    o_VehicleInfo.m_Doors = getCarNumberOfDoors();
                    o_VehicleInfo.m_RemainingBatteryTime = getRemainingBatteryTimeFromUser(i_VehicleType);
                    break;
            }
        }

        private void printCarNumberOfDoorsMenu()
        {
            Console.Write(string.Format(@"plese choose Doors number:
2.Two
3.Three
4.Four
5.Five
Press 2 or 3 or 4 or 5.
Your choice: "));
        }
        private eNumOfDoors getCarNumberOfDoors()
        {
            int userChoice = 0;
            eNumOfDoors type = eNumOfDoors.Two;
            printCarNumberOfDoorsMenu();

            bool isValidInput = false;

            while (!isValidInput)
            {
                getMenuChoice(ref userChoice, ref isValidInput, 2, 5);
            }

            if (userChoice == 2)
            {
                type = eNumOfDoors.Two;
            }
            else if (userChoice == 3)
            {
                type = eNumOfDoors.Three;
            }
            else if (userChoice == 4)
            {
                type = eNumOfDoors.Four;
            }
            else if (userChoice == 5)
            {
                type = eNumOfDoors.Five;
            }           
            return type;
        }

        private void printCalColorsMenu()
        {
            Console.Write(string.Format(@"plese choose your car color:
1.White,
2.Yellow,
3.Red,
4.Black
press 1 or 2 or 3 or 4.
Your choice: "));
        }

        private eCarColor getCarColor()
        {
            int userChoice = 0;
            eCarColor type = eCarColor.White;
            printCalColorsMenu();
            
            bool isValidInput = false;

            while (!isValidInput)
            {
                getMenuChoice(ref userChoice, ref isValidInput, 1, 4);
            }
            
            if (userChoice == 1)
            {
                type = eCarColor.White;
            }
            else if (userChoice == 2)
            {
                type = eCarColor.Yellow;
            }
            else if (userChoice == 3)
            {
                type = eCarColor.Red;
            }
            else if (userChoice == 4)
            {
                type = eCarColor.Black;
            }

            return type;
        }

        private int getMotorcycleEnginevolumeFromUser()
        {
            int motorVolume = 0;
            bool isValidInput = false;
            Console.Write("Enter Motorcycle Engine volume: ");
            while (!isValidInput)
            {
                 getMenuChoice(ref motorVolume, ref isValidInput, 1, 2000);
            }            
            return motorVolume;
        }  
        private void printTruckContainDangerMenu()
        {
            Console.Write(string.Format(@"is your Truck contains dangerous materials?
1.yes
2.no
press 1 or 2.
Your choice: "));
        }
        private bool isTruckContainsDangerousMaterials()
        {
            int userChoice = 0;
            printTruckContainDangerMenu();
            bool isValidInput = false, isTruckContainsDangerousMaterials = false ;

            while (!isValidInput)
            {
                getMenuChoice(ref userChoice, ref isValidInput, 1, 2);
            }

            if (userChoice == 1)
            {
                isTruckContainsDangerousMaterials = true;
            }
            else
            {
                isTruckContainsDangerousMaterials = false;
            }

            return isTruckContainsDangerousMaterials;            
        }

        private void printMotorcycleLicenceMenue()
        {
            Console.Write(string.Format(@"plese choose your licene type:?
1.A1,
2.A2,
3.AA,
4.B1
press 1 or 2 or 3 or 4.
Your choice: "));
        }
        private eMotorciclyLicenceType getMotorcycleLicenceTypeFromUser()
        {
            int userChoice = 0;
            eMotorciclyLicenceType type = eMotorciclyLicenceType.A1;
            printMotorcycleLicenceMenue();
            bool isValidInput = false;

            while (!isValidInput)
            {
                getMenuChoice(ref userChoice, ref isValidInput, 1, 4);
            }

            if (userChoice == 1)
            {
                type = eMotorciclyLicenceType.A1;
            }
            else if (userChoice == 2)
            {
                type = eMotorciclyLicenceType.A2;
            }
            else if (userChoice == 3)
            {
                type = eMotorciclyLicenceType.AA;
            }
            else if (userChoice == 4)
            {
                type = eMotorciclyLicenceType.B1;
            }

            return type;
        }
        private int truckCargoVolume()
        {
            int TruckCargo = 0;
            bool isValidInput = false;
            Console.Write("Enter Truck Cargo volume: ");
            while (!isValidInput)
            {
                getMenuChoice(ref TruckCargo, ref isValidInput, 0, 32767);
            }
            return TruckCargo;
        }

        private void printFuelMenue()
        {
            Console.Write(string.Format(@"plese choose your Fuel Type:
1. Soler
2. Octan95
3. Octan96
4. Octan98
press 1 or 2 or 3 or 4.
Your choice: "));
        }

        private eFuelType getFuelTypeFromUser()
        {
            eFuelType c_FuelType = eFuelType.Soler;
            int userChoice = 0;
            bool isValidInput = false;
            printFuelMenue();
            while (!isValidInput)
            {
                getMenuChoice(ref userChoice, ref isValidInput, 1, 4);
            }

            if (userChoice == 2)
            {
                c_FuelType = eFuelType.Octan95;
            }
            if (userChoice == 3)
            {
                c_FuelType = eFuelType.Octan96;
            }
            if (userChoice == 4)
            {
                c_FuelType = eFuelType.Octan98;
            }
            return c_FuelType;
        }
       private float getRemainingBatteryTimeFromUser(eVehicleType i_Type)
       {
            float minCapacity = 0;
            float maxCapacity = 6.4f;
            switch (i_Type)
            {
                case eVehicleType.CarElectric:
                    maxCapacity = 5.2f;
                    break;
                case eVehicleType.MotorcycleElectric:
                    maxCapacity = 2.6f;
                    break;              
            }
            float batteryCapacity = 0;
            bool isValidInput = false;
            Console.Write("Enter current Battery capacity: ");

            while (!isValidInput)
            {
                getCapacityFromUser(ref batteryCapacity, ref isValidInput, minCapacity, maxCapacity);
            }
            return batteryCapacity;
            
       }

        private void getCapacityFromUser(ref float o_FuelCapacity, ref bool o_IsValidInput, float i_MinVal, float i_MaxVal)
        {
            try
            {
                o_IsValidInput = getUserChoiceCapacity(out o_FuelCapacity, i_MinVal, i_MaxVal);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a number.");
            }
            catch (ValueOutOfRangeException)
            {
                Console.WriteLine(string.Format(@"Error: The number should be between {0} to {1}", i_MinVal, i_MaxVal));
            }
            catch (Exception i_OtherExeption)
            {
                Console.WriteLine(string.Format(@"Unknown error occured: 
{0}", i_OtherExeption.Message));
            }
        }

        private bool getUserChoiceCapacity(out float o_FuelCapacity, float i_MinVal, float i_MaxVal)
        {
            if (!float.TryParse(Console.ReadLine(), out o_FuelCapacity))
            {
                throw new FormatException();
            }

            if (o_FuelCapacity < i_MinVal || o_FuelCapacity > i_MaxVal)
            {
                throw new ValueOutOfRangeException(new Exception(), i_MaxVal, i_MinVal);
            }

            return true;
        }


        private float getCurrentFuelCapacityFromUser(eVehicleType i_Type)
        {
            float minCapacity = 0;
            float maxCapacity =6.4f;
            switch(i_Type)
            {
                case eVehicleType.Truck:
                    maxCapacity = 135;
                    break;
               case eVehicleType.MotorcycleFuel:
                    maxCapacity = 6.4f;
                    break;
                case eVehicleType.CarFuel:
                    maxCapacity = 46;
                    break;

            }
                
            float fuelCapacity = 0;
            bool isValidInput = false;
            Console.Write("Enter current Fuel capacity: ");
            while (!isValidInput)
            {
                getCapacityFromUser(ref fuelCapacity, ref isValidInput, minCapacity, maxCapacity);
            }
            return fuelCapacity;
        }
        private void getVehicleTypeFromUser(out eVehicleType userVehicleType)
        {
            int userChoice = 0;
            bool validInput = false;
            userVehicleType = eVehicleType.Truck;

            printVehicleTypeMenu();            

            while (!validInput)
            {
                    getMenuChoice(ref userChoice, ref validInput, 1, 5);                                             
            }

            if (userChoice == 1)
            {
                userVehicleType = eVehicleType.Truck;
            }
            else if (userChoice == 2)
            {
                userVehicleType = eVehicleType.CarFuel;
            }
            else if (userChoice == 3)
            {
                userVehicleType = eVehicleType.CarElectric;
            }
            else if (userChoice == 4)
            {
                userVehicleType = eVehicleType.MotorcycleFuel;
            }
            else if (userChoice == 5)
            {
                userVehicleType = eVehicleType.MotorcycleElectric;
            }

        }

        private void printVehicleTypeMenu()
        {
            Console.Write(string.Format(@"plase enter your car type:
1. Truck
2. Fuel Car
3. Electric Car
4. Fuel Motorcycle
5. Electric Motorcycle
Your choice: "));
        }
        private void inflateVehicleToMax()
        {
            string currLicenceNumber = getLicenceNumber();
            try
            {
                m_Garage.InflateAirToMax(currLicenceNumber);
                Console.WriteLine(string.Format(@"
Inflate To Max for '{0}' Vehicle Sucess!
", currLicenceNumber));
            }
            catch (VehicleDoesNotInGarageException)
            {
                Console.WriteLine(string.Format(@"
Error: This License does not exist in this Garage!
"));
            }
            catch (Exception i_CurrentExeption)
            {
                Console.WriteLine(string.Format(@"Unknown error occured: 
{0}
", i_CurrentExeption.Message));

            }
        }

        private float getFuelForRefeul()
        {
            float userChoice = 0;
            bool isValidInput = false;
            Console.Write("Enter amount to refeul: ");
            while (!isValidInput)
            {
                getCapacityFromUser(ref userChoice, ref isValidInput, 0, 32767);
            }

            return userChoice;
        }

        private float getElectrictyForRecharge()
        {
            float userChoice = 0;
            bool isValidInput = false;
            Console.Write("Enter amount to Recharge in minutes: ");
            while (!isValidInput)
            {
                getCapacityFromUser(ref userChoice, ref isValidInput, 0, 32767);
            }

            return userChoice;

        }

        private void refuelVehicle()
        {
            string currLicenceNumber = getLicenceNumber();
            eFuelType currFuelType = getFuelTypeFromUser();
            float refuelAmount = getFuelForRefeul();


            try
            {
                float newAmount;
                m_Garage.RefuelVehicle(currLicenceNumber, currFuelType, refuelAmount ,out newAmount);
                Console.WriteLine(string.Format(@"
Refuel: '{0}' Vehicle, current amount of fuel is {1}. 
", currLicenceNumber, newAmount));
            }
            catch (VehicleDoesNotInGarageException)
            {
                Console.WriteLine(string.Format(@"
Error: This License does not exist in this Garage!
"));
            }
            catch (FormatException)
            {
                Console.WriteLine(string.Format(@"
Error: This Engine Cannot Refuel with fuel!
"));
            }
            catch (IncorrectFuelTypeException)
            {
                Console.WriteLine(string.Format(@"
Error: This Engine Cannot Refuel with this fuel Type!!
"));
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(string.Format(@"
Value Out Of Range! Range should be between {0} - {1} 
", ex.MinValue , ex.MaxValue));
            }
            catch (Exception i_CurrentExeption)
            {
                Console.WriteLine(string.Format(@"Unknown error occured: 
{0}
", i_CurrentExeption.Message));

            }
        }

        private void rechargeVehicle()
        {
            string currLicenceNumber = getLicenceNumber();
            
            float rechargeAmount = getElectrictyForRecharge();


            try
            {
                float newAmount;
                m_Garage.rechargeVehicle(currLicenceNumber, rechargeAmount, out newAmount);
                Console.WriteLine(string.Format(@"
Recharge: {0} Vehicle, current amount of time left in Battary is {1:F3}. 
", currLicenceNumber, newAmount));
            }
            catch (VehicleDoesNotInGarageException)
            {
                Console.WriteLine(string.Format(@"
Error: This License does not exist in this Garage!
"));
            }
            catch (FormatException)
            {
                Console.WriteLine(string.Format(@"
Error: This Engine Cannot Refuel with fuel!
"));
            }
            catch (IncorrectFuelTypeException)
            {
                Console.WriteLine(string.Format(@"
Error: This Engine Cannot Refuel with this fuel Type!!
"));
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(string.Format(@"
Value Out Of Range! Range should be between {0} - {1} 
", ex.MinValue, ex.MaxValue));
            }
            catch (Exception i_CurrentExeption)
            {
                Console.WriteLine(string.Format(@"Unknown error occured: 
{0}
", i_CurrentExeption.Message));

            }
        }

    }

    


}

