# WPF-graph-of-the-power-grid
Computer graphics subject

Graf mreže je potrebno iscrtati na osnovu Geographic.xml fajla.
Graf mreže aproksimira mrežu na ortogonalni prikaz u vidu grid-a. Prvo što je potrebno je da se
definiše (“zamišljeni”) grid - što vise redova i kolona, to će prikaz biti detaljniji. Potom se učitavaju
koordinate iz xml fajla i crtaju se čvorovi, tako što se aproksimiraju na najbližu tačku sa grida - tačka
je presek horizontalnih i vertikalnih “linija”.
Svaki čvor se aproksimira na grid. Čvorovi se iscrtavaju na mreži tako što se iscrtava slika (grafički
element) koja će predstavljati datu vrstu čvora (Substation, Node, Switch). Za svaki čvor se prikazuje
ToolTip sa inforamacijom koji element se tu nalazi. Levim klikom miša na čvor, on će se uvećati 10
puta u odnosu na svoju redovnu veličinu i potom jednako smanjiti.
1-a: Čvorovi se aproksimiraju na najbližu tačku mreže i u tom slučaju se čvorovi mogu preklapati. Ako
dođe do preklapanja, na datom mestu se iscrtava neka posebna sličica koja označava grupu, a u
ToolTip-u se prikazuju informacije o svakom elementu koji se tu nalazi. (2 poena)
1-b: Čvorovi se aproksimiraju na najbližu slobodnu tačku mreže. U ovom slučaju treba voditi računa
o minimalnoj dimenziji mreže kako bi bilo prostora za sve čvorove.
Predlog: minimum 100x100 (3 poena)
Vodovi se crtaju kao prave linije i ukoliko je potrebno, linija mora da skreće samo pod pravim uglom.
Posmatraju se samo start i end nodes u linijama. Vertices se ignorišu. Iscrtavaju se samo one linije čiji
start i end node postoje u kolekcijama čvorova. Ostali vodovi se ignorišu. Treba ignorisati ponovno
iscrtavanje vodova izmedju dva ista čvora. Linija uvek mora da kreće iz centra čvora, ne iz gornjeg
levog ugla (pozicije iscrtavanja) čvora.
2-a: Vod se iscrtava kao najkraća putanja između dva čvora (bilo koja najkraća). Ukoliko je na
zadatom mestu već iscrtan vod, ne crtati novi preko njega. Ako dođe do preseka vodova, označiti
presek. (4 poena)
2-b: Nalazi se najkraći mogući put BEZ presecanja sa već postojećim iscrtanim vodovima (BFS
algoritam). U drugom prolazu se iscrtavaju vodovi za koje u prvom prolazu nije bilo moguće naći put
bez presecanja i tada se i oni iscrtavaju uz označavanje tačaka preseka. Algoritam započeti od neka
dva čvora koja imaju najmanju udaljenost na gridu. Naći ih automatski ili ručno. (6 poena)
Desnim klikom na vod između dva čvora treba ponuditi opciju da čvorovi povezani tim vodom budu
obojeni različitim bojama od ostalih čvorova kako bi korisnik znao koji su čvorovi povezani tim
vodom.
Pored svega ovoga, potrebno je omogućiti zumiranje grida tako da se pomoću skrol-barova može
pomerati pogled nad zumiranim delom grida, kao i da se grid može „odzumirati“.
Napomena: Tooltip-ovi prikazuju ID i ime entiteta (status za Switch), a prikazuju se i za veze
(vodove).
BFS: Grid se nekako mora čuvati u vidu podataka – matrica / niz nizova / liste. BFS uzima početni
čvor i proverava da li je to ciljni čvor. Potom uzima decu tog čvora i proverava da li je neki od njih
ciljni. Deca jednog čvora su susedni red i susedna kolona. Potrebno je negde imati listu svih
predjenih čvorova, da se ne bi radio dupli posao. Ukoliko prvo dete nije ciljni čvor, njegova deca se
dodaju na listu čvorova za dalju proveru, a potom se predje na drugo dete itd. Kada se dodje do
ciljnog cvora, vrati se čitava putanja kojom se pristiglo do cilja –> čuva se i lista predjenih čvorova
koja su vodila do trenutnog čvora. 
