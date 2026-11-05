Kodmässigt, försök att jobba med separation! Exakt hur ni separerar spelar mindre roll i nuläget, men se till så att det finns en separation mellan:
Användargränssnittsrelaterad kod (Input output till konsolen och allt som har med det att göra)
Applikationslogik (sk. Business logic). Vi kan även tänka på det som affärsregler. Exempelvis “Det ska inte gå att hyra en film om det inte finns något fysisk exemplar att hyra”, eller “Det går inte att hyra film om man inte betalat sin skuld”.
databasrelaterad kod.
En del kod kommer oundvikligen behöva delas mellan alla dessa delar, exempelvis klasser för Movie, Customer osv, så kallade domän-objekt. Det är ok! I såna objekt kan viss domän-logik finnas, exempelvis “Det ska inte gå att skapa en kund utan namn och telefonnummer”.
Så, prata tillsammans i gruppen om hur ni tänker kring separation i ert projekt! Om ni tycker det är svårt, försök hitta konkreta exempel på vad som är svårt, vad är svårt att veta var det ska vara nånstans? Varför är det svårt? Vad är det som gör det otydligt? Och be sedan om hjälp om ni inte kommer vidare :)
