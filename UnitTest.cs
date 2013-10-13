using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using SixtyFourT.BAL;

namespace SixtyFourT.Test
{

  [TestClass]
  public class UnitTest
  {
    //Define controller
    Controller _controller = new Controller();
    
    //Initialise elevators 2 Initialised at the momemnt
    //Maximum four we can use in the system
    Elevator elevator1 = new Elevator();
    Elevator elevator2 = new Elevator();
    
    //hash table defined for pending requests which will be stored each elevator wise
    Hashtable hashtable = new Hashtable();
    
    //Floor initialsed at 30 floor. Floor returns with top and bottom button enabled
    Floor floor30 = new Floor(30);

    //Floor initialsed at 80 floor. Floor returns with top and bottom button enabled
    Floor floor80 = new Floor(80);
    
    //Floor initialsed at 100 floor. Foor returns with Top button disabled
    Floor floor100 = new Floor(100);

    //Floor initialsed at 0 floor Foor returns with bottom button disabled
    Floor floor0 = new Floor(0);

    //constructor starts timer
    public UnitTest()
    {
      _controller.StartController();
    }

    //This method request from floor and returns elevator
    [TestMethod]
    public void FloorRequestReturnElevator()
    {
      //This method request from floor and returns elevator
      elevator1 = _controller.ServiceRequest(floor30);
      Assert.AreEqual(elevator1.CureentFloor, 30);
    }

    //This test checks elevator is in the motion and moving up
    [TestMethod]
    public void ElevatorCheckMovingUp()
    {
      //This method request from floor and returns elevator
      elevator1 = _controller.ServiceRequest(floor30);
      //Elevator1 requests: From floor 30, direction up, up 2 floors
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);
      Assert.AreEqual(elevator1.Status, Utility.Status_Moving_Up);
    }

    //This test checks elevator is in the motion and moving down
    [TestMethod]
    public void ElevatorCheckMovingDown()
    {
      //This method request from floor and returns elevator
      elevator2 = _controller.ServiceRequest(floor80);
      //Elevator2 requests: From floor 30, direction down, down 5 floors
      elevator2.CureentFloor = 80;
      elevator2.Direction = 0;
      elevator2.FloorCount = 5;
      _controller.StartService(elevator2);
      Assert.AreEqual(elevator2.Status, Utility.Status_Moving_Down);
    }

    //This test checks elevator reached floor and elevator is in the opening door state
    [TestMethod]
    public void ElevatorCheckDoorOpening()
    {
      //This method request from floor and returns elevator
      elevator1 = _controller.ServiceRequest(floor30);
      //Elevator1 requests: From floor 30, direction up, up 2 floors
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);
      //2 floors wait for 6 secs
      System.Threading.Thread.Sleep(6500);
      Assert.AreEqual(elevator1.Status, Utility.Status_Door_Opening);
    }

    //This test checks elevator reached floor and elevator is in the closing door state
    [TestMethod]
    public void ElevatorCheckDoorClosing()
    {
      //This method request from floor and returns elevator
      elevator1 = _controller.ServiceRequest(floor30);
      //Elevator1 requests: From floor 30, direction up, up 2 floors
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);
      //2 floors wait for 6 +2 8 secs for door close
      System.Threading.Thread.Sleep(8500);
      Assert.AreEqual(elevator1.Status, Utility.Status_Door_Closing);
    }

    //This test checks elevator reached floor and elevator is in the dormant state
    [TestMethod]
    public void ElevatorCheckDormant()
    {
      //This method request from floor and returns elevator
      elevator1 = _controller.ServiceRequest(floor30);
      //Elevator1 requests: From floor 30, direction up, up 2 floors
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);
      //2 floors wait for 6 +2 + 2 = 10 secs for dormant elevator
      System.Threading.Thread.Sleep(10500);
      Assert.AreEqual(elevator1.Status, Utility.Status_Dormant);
    }

    //This test checks elevator moving up, returning its state while is in the motion
    [TestMethod]
    public void ElevatorCheckMovingUp1Floor()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      //Elevator1 requests: From floor 30, direction up, up 3 floors
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 3;
      _controller.StartService(elevator1);
      //1 floor wait for 3 secs for status of next floor
      System.Threading.Thread.Sleep(3500);
      Assert.AreEqual(elevator1.CureentFloor, 31);
    }

    //This test checks elevator moving up, returning its state while is in the motion
    [TestMethod]
    public void ElevatorCheckMovingUp2Floor()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      //Elevator1 requests: From floor 30, direction up, up 3 floors
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 3;
      _controller.StartService(elevator1);
      //2 floors wait for 6 secs for status of 2nd floor
      System.Threading.Thread.Sleep(6500);
      Assert.AreEqual(elevator1.CureentFloor, 32);
    }

    //This test checks elevator moving down, returning its state while is in the motion
    [TestMethod]
    public void ElevatorCheckMovingDown1Floor()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      //Elevator1 requests: From floor 70, direction down, down 3 floors
      elevator1.CureentFloor = 70;
      elevator1.Direction = 0;
      elevator1.FloorCount = 3;
      _controller.StartService(elevator1);
      //1 floor wait for 3 secs for status of next floor
      System.Threading.Thread.Sleep(3500);
      Assert.AreEqual(elevator1.CureentFloor, 69);
    }

    //This test checks elevator moving down, returning its state while is in the motion
    [TestMethod]
    public void ElevatorCheckMovingDown2Floor()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      //Elevator1 requests: From floor 70, direction down, down 3 floors
      elevator1.CureentFloor = 70;
      elevator1.Direction = 0;
      elevator1.FloorCount = 3;
      _controller.StartService(elevator1);
      //2 floors wait for 6 secs for status of 2nd floor
      System.Threading.Thread.Sleep(6500);
      Assert.AreEqual(elevator1.CureentFloor, 68);
    }

    //This test checks top floor and top button disabled
    [TestMethod]
    public void TopFloorCheckUpButtonDisabled()
    {
      elevator1 = _controller.ServiceRequest(floor100);
      Assert.AreEqual(floor100.ButtonUp, false);
    }

    //This test checks top floor and bottom button is enabled
    [TestMethod]
    public void TopFloorCheckDownButtonEnabled()
    {
      elevator1 = _controller.ServiceRequest(floor100);
      Assert.AreEqual(floor100.ButtonDown, true);
    }

    //This test checks bottom floor and down button disabled
    [TestMethod]
    public void BottomFloorCheckDownButtonDisabled()
    {
      elevator1 = _controller.ServiceRequest(floor0);
      Assert.AreEqual(floor0.ButtonDown, false);
    }

    //This test checks bottom floor and up button disabled
    [TestMethod]
    public void BottomFloorCheckUpButtonEnabled()
    {
      elevator1 = _controller.ServiceRequest(floor0);
      Assert.AreEqual(floor0.ButtonUp, true);
    }

    //This test checks top and bottom enabled when floor 30
    [TestMethod]
    public void Floor30CheckUpDownButtonEnabled()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      bool upButton = floor30.ButtonUp;
      bool downButton = floor30.ButtonDown;
      bool expectedresult = true;
      bool finalResult = false;
      if (upButton && downButton)
      {
        finalResult = true;
      }
      Assert.AreEqual(finalResult, expectedresult);
    }

    //This test checks contoller make elevator disable
    [TestMethod]
    public void ControllerMakeElevatorDisable()
    {
      //enable disable elevator by chaing the new status
      //new status Status_Dormant equal to enable.
      //new status Status_Disabled equal to disabled.
      _controller.SetElevatorStatus(elevator1, Utility.Status_Disabled);
      Assert.AreEqual(elevator1.Status, Utility.Status_Disabled);
    }

    //This test checks no of outstanding request per elevator
    [TestMethod]
    public void OutstandingRequestCheck()
    {

      Elevator elevator3 = new Elevator();
      Elevator elevator4 = new Elevator();
      Floor floor10 = new Floor(10);
      Floor floor70 = new Floor(70);

      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);

      elevator2 = _controller.ServiceRequest(floor80);
      elevator2.CureentFloor = 30;
      elevator2.Direction = 1;
      elevator2.FloorCount = 2;
      _controller.StartService(elevator2);

      elevator3 = _controller.ServiceRequest(floor100);
      elevator3.CureentFloor = 30;
      elevator3.Direction = 1;
      elevator3.FloorCount = 2;
      _controller.StartService(elevator3);

      elevator4 = _controller.ServiceRequest(floor0);
      elevator4.CureentFloor = 30;
      elevator4.Direction = 1;
      elevator4.FloorCount = 2;
      _controller.StartService(elevator4);

      //Fifth outstanding request 
      elevator1 = _controller.ServiceRequest(floor70);

      //total number of outstanding is one
      int pendReqCount = elevator1.PendingRequests.Count;

      Assert.AreEqual(1, pendReqCount);
    }

    //This test checks no of outstanding request per elevator: 
    //check outstanding floor number 
    [TestMethod]
    public void OutstandingRequestCheckFloorNo()
    {

      Elevator elevator3 = new Elevator();
      Elevator elevator4 = new Elevator();
      Floor floor10 = new Floor(10);
      Floor floor70 = new Floor(70);

      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);

      elevator2 = _controller.ServiceRequest(floor80);
      elevator2.CureentFloor = 30;
      elevator2.Direction = 1;
      elevator2.FloorCount = 2;
      _controller.StartService(elevator2);

      elevator3 = _controller.ServiceRequest(floor100);
      elevator3.CureentFloor = 30;
      elevator3.Direction = 1;
      elevator3.FloorCount = 2;
      _controller.StartService(elevator3);

      elevator4 = _controller.ServiceRequest(floor0);
      elevator4.CureentFloor = 30;
      elevator4.Direction = 1;
      elevator4.FloorCount = 2;
      _controller.StartService(elevator4);

      //Fifth request outstanding
      elevator1 = _controller.ServiceRequest(floor70);

      string floorNumber = "";
      string direction;
      //loop through all pending request 
      foreach (DictionaryEntry entry in elevator1.PendingRequests)
      {
        floorNumber = entry.Key.ToString();
        direction = entry.Value.ToString();
        if (direction == "1")
        {
          direction = Utility.Status_Moving_Up;
        }
        else
        {
          direction = Utility.Status_Moving_Down;
        }
      }
      Assert.AreEqual(floorNumber, "70");
    }

    //This test checks no of outstanding request per elevator: 
    //check outstanding request floor direction ( 1 = up and 0 = down) 
    [TestMethod]
    public void OutstandingRequestCheckDirection()
    {

      Elevator elevator3 = new Elevator();
      Elevator elevator4 = new Elevator();
      Floor floor10 = new Floor(10);
      Floor floor70 = new Floor(70);

      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);

      elevator2 = _controller.ServiceRequest(floor80);
      elevator2.CureentFloor = 30;
      elevator2.Direction = 1;
      elevator2.FloorCount = 2;
      _controller.StartService(elevator2);

      elevator3 = _controller.ServiceRequest(floor100);
      elevator3.CureentFloor = 30;
      elevator3.Direction = 1;
      elevator3.FloorCount = 2;
      _controller.StartService(elevator3);

      elevator4 = _controller.ServiceRequest(floor0);
      elevator4.CureentFloor = 30;
      elevator4.Direction = 1;
      elevator4.FloorCount = 2;
      _controller.StartService(elevator4);

      //Fifth outstanding request 
      elevator1 = _controller.ServiceRequest(floor70);

      string floorNumber = "";
      string direction = "";
      //loop through all pending request 
      foreach (DictionaryEntry entry in elevator1.PendingRequests)
      {
        floorNumber = entry.Key.ToString();
        direction = entry.Value.ToString();
        if (direction == "1")
        {
          direction = Utility.Status_Moving_Up;
        }
        else
        {
          direction = Utility.Status_Moving_Down;
        }
      }
      Assert.AreEqual(direction, Utility.Status_Moving_Down);
    }

    //This test checks for controller can configure the elevator and send to requested floor 
    [TestMethod]
    public void ControllerConfigureElevatorFloor()
    {
      floor30.CureentFloor = 45;
      _controller.ConfigureElevatorForFloor(elevator1, floor30);
      Assert.AreEqual(elevator1.CureentFloor, 45);
    }

    //This is not a test method.
    //This method to show contoller can see tatus of all elevators
    private void ControllerGetAllElevatorsStatus()
    {
      //by looping though the all elevatros 
      foreach (Elevator elevator in _controller.elevators)
      {
        //get the status of each elever
        //elevator.Direction
        //elevator.CureentFloor
        //etc...
      }
    }
  }
}
