--select count(*) from tickets limit 100
--select * from tickets where passenger_id is not null and ticket_no > '0005432000322' limit 100
--select * from troupers where passenger_name='ELENA BELOVA'
-- select count(*) nullCountsNot from tickets where passenger_id is not null;

select book_ref from tickets limit 100

-- create index idx_temp_PassengerNameInTickets on tickets(passenger_name,passenger_id);
-- drop index idx_temp_PassengerNameInTickets;
-- select * from tickets where passenger_id is null order by passenger_id desc limit 3;
--select count(*) from troupers where "Id" > '0qq02g9c0ow4uf8sf6d57mgt9' limit 2
-- select * from troupers where passenger_name='VLADISLAV PAVLOV'

select traveller_id from travellers limit 100;
-- do $$
--     declare r record;
--   begin
--         for r in select passenger_name,"Id" from troupers
--         loop
--             --update tickets set passenger_id=r."Id" where passenger_name=r.passenger_name and passenger_id is null;
--             call proc_updatetickets_passengeridcolumn(r.passenger_name,r."Id");
--             commit ;
--             end loop;
--     end;
-- $$
-- alter table travellers rename "Id" to traveller_id;
SELECT  vtravellers.passenger_name,  vbookings.total_amount, t0.ticket_no,  vtravellers.traveller_id,  vbookings.book_ref, t2.seat_class, t2.arrival_airport, t2.status, t2.scheduled_departure, t2.ticket_no, t2.flight_id, t2.class_id, t2.flight_id0
 FROM (SELECT  vtickets.ticket_no,  vtickets.book_ref,  vtickets.passenger_id FROM bookings.tickets AS vtickets  WHERE  vtickets.passenger_id = '0qpzc0wp1qx3ydkwnmwvg7ama' ORDER BY  vtickets.passenger_id LIMIT 5  ) AS t0
 LEFT JOIN bookings.travellers AS vtravellers ON t0.passenger_id =  vtravellers.traveller_id  INNER JOIN bookings.bookings AS vbookings ON t0.book_ref =  vbookings.book_ref
 LEFT JOIN (SELECT s.flight_class AS seat_class, f.arrival_airport, f.status, f.scheduled_departure, t3.ticket_no, t3.flight_id, s.class_id, f.flight_id AS flight_id0
FROM bookings.ticket_flights AS t3 LEFT JOIN bookings.seat_classes AS s ON t3.fare_condition_id = s.class_id
INNER JOIN bookings.flights AS f ON t3.flight_id = f.flight_id  ) AS t2 ON t0.ticket_no = t2.ticket_no
ORDER BY t0.passenger_id, t0.ticket_no,  vtravellers.traveller_id,  vbookings.book_ref, t2.ticket_no, t2.flight_id, t2.class_id

-- //find all the airpots anna_ti has ever been to

select count(*) from ailines_bookings.bookings

SELECT pg_size_pretty( pg_database_size('airlines') )