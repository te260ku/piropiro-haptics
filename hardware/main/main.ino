#include <WiFi.h>
#include <WiFiUdp.h>

const char* ssid = "ssid";
const char* password = "pwd";

const char* clientAddress = "xxx.xxx.xx.xx";
const int clientPort = 22222;
const int serverPort = 22224;  // espのポート番号

WiFiUDP udp;

byte recvbuf[1024];
byte sendbuf[1024];

int recvbufSize = 3;
int sendbufSize = 1;

int c = 0;
float val = 0;

// 2バイト以上のデータタイプはビットシフトしてバイト分割して送る
// floatは共用体を利用して送る
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
  uint8_t bin[sizeof(float) * 3];
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
    int getsize = udp.read(recvbuf, recvbufSize);
    Serial.print(recvbuf[0], HEX);
    Serial.print(recvbuf[1], HEX);
    Serial.print(recvbuf[2], HEX);
    Serial.print("\n");
  }
}

void sendUDP (byte buf[]) {
  udp.beginPacket(clientAddress, clientPort);
  udp.write(buf, sendbufSize);
  udp.endPacket();
  Serial.println("send");
}

int getDistance() {
  // 平均を取る
  for (int i=0 ; i<1000; i++) {
      val += analogRead(A6);
  }
  // 補正式
  int dist = 1950114 * pow(val/1000, -1.256676);
  // 最大値を固定
  if (dist > 255) {
    dist = 255;
  }
  return dist;
}

 
void loop() {
  // 1秒に1回実行
  if (c >= 10) {
    val = 0;
    int dist = getDistance();
    sendbuf[0] = dist;

    sendUDP(sendbuf);

    c = 0;
  }
  
  delay(100);
  c++;

}
