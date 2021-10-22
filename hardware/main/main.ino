#include <WiFi.h>
#include <WiFiUdp.h>

const char* ssid = "ssid";
const char* password = "pwd";

const char* clientAddress = "xxx.xxx.xx.xx";
const int clientPort = 22222;
const int serverPort = 22224;  // espのポート番号

WiFiUDP udp;


boolean sendFlag = false;

byte recvbuf[1024];
byte sendbuf[1024];

int recvbuf_size = 3;
int sendbuf_size = 1;

int c = 0;
float ans = 0;
float dist1;
float dist2;
float Vcc = 5.0;

typedef union {
  float val;
  byte binary[4];
} uf;

union Position
{
    struct {
        float x;
        float y;
        float z;
    };
    uint8_t bin[sizeof(float) * 3]; // -> 4 * 3 = 12
};






void setup() {
  Serial.begin(115200);

  Serial.println("Connecting to WiFi network: " + String(ssid));
  WiFi.disconnect(true, true);
  delay(500);
  
  WiFi.begin(ssid, password);
  while( WiFi.status() != WL_CONNECTED) {
    delay(500);  
  }  
  udp.begin(serverPort);
  delay(500);  

  randomSeed(1);
}


void receiveUDP(){
  int packetSize = udp.parsePacket();
  if (packetSize > 0) {
    int getsize = udp.read(recvbuf, recvbuf_size);
    Serial.print(recvbuf[0], HEX);
    Serial.print(recvbuf[1], HEX);
    Serial.print(recvbuf[2], HEX);
    Serial.print("\n");
  }
}

void sendUDP(){
  //２バイト以上のデータタイプはビットシフトしてバイト分割して送る
  //float は共用体を利用して送る
  if (sendFlag) {
    udp.beginPacket(clientAddress, clientPort);
    udp.write(sendbuf, sendbuf_size);
    udp.endPacket();
    Serial.println("send");
    sendFlag = false;
  }
}

int getDistance() {
  for (int i=0 ; i<1000; i++) {
      ans  = ans + analogRead(A6);
  }
  int dist1 = 1950114 * pow(ans/1000, -1.256676);
  if (dist1 > 255) {
    dist1 = 255;
  }
  return dist1
}

 
void loop() {

  // 1秒に1回実行
  if (c >= 10) {
    
    ans = 0;

    int dist = getDistance();
    sendbuf[0] = dist;

    sendUDP();

    c = 0;
    sendFlag = true;
  }
  

  delay(100);
  c++;
  
}
