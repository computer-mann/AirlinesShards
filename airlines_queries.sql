-- select count(*) from bookings
use test

alter view aircrafts rename to vw_aircrafts;
alter view airports rename to vw_airpots;
alter view flights_v rename to vw_flights;
alter view routes rename to vw_routes;
--select distinct passenger_name namess from tickets --where passenger_name='ELENA ZAKHAROVA'-- limit 20

--select * from tickets limit 20 order by passenger_name

--select * from tickets limit 20
select * from tickets where passenger_name in (select passenger_name from tickets limit 20) order by passenger_name
create view vw_tickets_lim20 as select * from tickets limit 20;
select * from vw_tickets_lim20 order by passenger_name
--select * from vw_tickets_lim20 order by passenger_name desc

--select fare_conditions,count(*) flights from ticket_flights group by fare_conditions order by flights

select * from ticket_flights order by ticket_no desc limit 150

--select * from tickets where passenger_name='ELENA ZAKHAROVA'
--select * from tickets where book_ref='D3C4B3' --2.259 secs
--select * from tickets where ticket_no='0005432286543'
--select * from tickets where passenger_id='1167 485656' -- no index=2.313
--create index idx_temp_passengerid_in_tickets on tickets(passenger_id) WITH (deduplicate_items = off)
--drop index idx_temp_passengerid_in_tickets
--select * from tickets order by passenger_id limit 120
select * from bookings order by book_ref limit 120
 select * from ticket_flights where ticket_no > '0005435999870' --order by ticket_no desc limit 120