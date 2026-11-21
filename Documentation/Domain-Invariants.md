En kopia som är uthyrd kan inte hyras ut
EN kopia som är uthyrd kan inte skickas till utgivaren
En film kan inte läggas till två gånger
Invariant 1: Återlämningsdatumet kan inte vara tidigare än
uthyrningsdatumet (ReturnedAt >= RentedAt).
• Invariant 2: En filmkopia kan inte återlämnas två gånger (om den
redan är återlämnad kastar den InvalidOperationException).
Du kan inte återlämna en kopia som inte är uthyrd
IsActive=false får inte hyras ut
Återlämningsdatum måste vara null och inte vara satt om inte ett
uthyrningsdatum finns?
