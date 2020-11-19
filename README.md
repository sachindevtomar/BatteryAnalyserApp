# BatteryAnalyserApp


## Battery Consumption Calculation

![Battery Consumption Calculation](https://github.com/sachindevtomar/BatteryAnalyserApp/raw/master/CalculationSample.PNG)

1. In the above example, I took example of one device whose battery readings at different times are shown.
2. I am getting all battery consumptions before every charging (increase in battery level).
3. Converting that in 24 hours (i.e. 5% decreased in 4 hours then 30% will be decreased in 24 hours)
4. Taking average of all the values and it will be the average battery consumption for that device.
5. will repeat #2-#4 for each device (Using serialNumber)

## Result 
Following are the analysis After running solution on the Sample data

### Total Devices(grouped by SerialNumber): 59
### Faulty Devices (Battery Consumption > 30 in a day): 14
### Good Devices (Battery Consumption <= 30 in a day): 42
### Unknown Devices (With only single entry available in data): 3

## How To Run

* Clone the solution.
* Build and Run the solution (F5)
