# WebSwitchESP

Le projet permet le controle via le Web de 14 relais. Grace au driver ASCOM, il est pilotable a partir des softs d'astronomie compatibles.
La connection au reseau se fait au choix, soit en WIFI soit par Ethernet via un module ENC28J60.
Le projet se compose de deux PCB:
  - une carte interface: https://easyeda.com/gronniercedric/webswitchesp
  - une carte relais: https://easyeda.com/gronniercedric/webswitchcarterelais
La carte relais, recoit les relais, les transistors de commande des relais, le connecteur d'alimentation 12V et un connecteur la relaint à la carte interface.
Elle comporte une protection contre les inversion de polarité à bas e de mosfet et un fusible rearmable de 0.75A.
La carte interface recoit, l'ESP32 NodeMCU (plusieurs largeur de modules prevus), le regulateur de tension 5V pour l'ESP32 et l'ENC28J60, un connecteur vesrs la 
carte relais et un connecteur vers l'ENC28J60.
