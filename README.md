# AccountingDemo

this  is a simple project for demo.
in this project we have two microservices...voucher and  journal.... we creat delete and update a voucher then we will send this operation to jouranl using  rabbittmq.i use of ddd approuch and  cqrs   along  with  entityframeworkcore-codefirst and sql server.
to run project Follow the recipe below:

1- first  go  to the AccountingVoucher  folder  and  set the AccountingVoucher.Api project  as setStartupProject

2- go to  the  appsetting.json  and set   database connection string  according to your wish.

3- go to  Package Manager Console  and  set defaultproject to AccountingVoucher.Infraustructure

4-Run this command at the command line:Add-Migration initialdatabase -context -vouchercontext

5-Run :Update-database -verbose   or update-database -context vouchercontext

do this   work  for AccountingJournal Microservice for creating your database:

4-Run this command at the command line:Add-Migration initialdatabase -context -journalcontext


go to  solution and rigth click  at it   go to Multi StartUp  Project option and  select  AccountingVoucher.API and  AccountingJournal.API  toghther

run doucker desktop 
run  this command  for danlowad   docker image and run those image  in rabbitt countiner 

docker run --rm -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management


now  run  the project  and you must  see swaggers of those microservices

***for update voucher give voucher number between ""

***every crud operation must be reflect  in journal database

***i write a few test fot  AccountingVoucher using xunit,Moq,AutoFixture and  ...
*** i use of vs 2022 
*** refresh browser to see swagger
