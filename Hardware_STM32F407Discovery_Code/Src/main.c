
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  ** This notice applies to any and all portions of this file
  * that are not between comment pairs USER CODE BEGIN and
  * USER CODE END. Other portions of this file, whether 
  * inserted by the user or by software development tools
  * are owned by their respective copyright owners.
  *
  * COPYRIGHT(c) 2020 STMicroelectronics
  *
  * Redistribution and use in source and binary forms, with or without modification,
  * are permitted provided that the following conditions are met:
  *   1. Redistributions of source code must retain the above copyright notice,
  *      this list of conditions and the following disclaimer.
  *   2. Redistributions in binary form must reproduce the above copyright notice,
  *      this list of conditions and the following disclaimer in the documentation
  *      and/or other materials provided with the distribution.
  *   3. Neither the name of STMicroelectronics nor the names of its contributors
  *      may be used to endorse or promote products derived from this software
  *      without specific prior written permission.
  *
  * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
  * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
  * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
  * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
  * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
  * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
  * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
  * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
  * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
  * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
  *
  ******************************************************************************
  */
/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "stm32f4xx_hal.h"

/* USER CODE BEGIN Includes */
#include "rc522.h"
#include "string.h"
/* USER CODE END Includes */

/* Private variables ---------------------------------------------------------*/
SPI_HandleTypeDef hspi2;

TIM_HandleTypeDef htim1;
TIM_HandleTypeDef htim2;
TIM_HandleTypeDef htim3;
TIM_HandleTypeDef htim4;
TIM_HandleTypeDef htim5;
TIM_HandleTypeDef htim8;
TIM_HandleTypeDef htim9;

UART_HandleTypeDef huart2;
DMA_HandleTypeDef hdma_usart2_tx;
DMA_HandleTypeDef hdma_usart2_rx;

/* USER CODE BEGIN PV */
/* Private variables ---------------------------------------------------------*/
/*----Khai bao bien su dung cho doc cam bien -----*/	
#define sensorCount 5
#define maxValue 1000
uint32_t tick = 0;
uint16_t sensorValues[sensorCount];
uint16_t sensorPins[sensorCount] = {GPIO_PIN_0,GPIO_PIN_1,GPIO_PIN_2,GPIO_PIN_3,GPIO_PIN_4};//GPIOA
char calibInitialized = '0'; // Calibration status
uint16_t minCalibValues[sensorCount]; // Lowest readings seen during calibration
uint16_t maxCalibValues[sensorCount]; // Highest readings seen during calibration
uint16_t position;
/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_DMA_Init(void);
static void MX_SPI2_Init(void);
static void MX_TIM1_Init(void);
static void MX_TIM2_Init(void);
static void MX_TIM3_Init(void);
static void MX_TIM4_Init(void);
static void MX_TIM5_Init(void);
static void MX_TIM8_Init(void);
static void MX_TIM9_Init(void);
static void MX_USART2_UART_Init(void);
static void MX_NVIC_Init(void);
                                    
void HAL_TIM_MspPostInit(TIM_HandleTypeDef *htim);
                                

/* USER CODE BEGIN PFP */
/* Private function prototypes -----------------------------------------------*/
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim);
void QTRPins_OUTPUT_Mode(void);
void QTRPins_INPUT_Mode(void);
void QTRSensorsRead(uint16_t *sensorValues);
void QTRSensorsCalibrate(void);
void QTRSensorsReadCalibrated(uint16_t *_sensorValues);
uint16_t QTRSensorsReadLine(uint16_t *_sensorValues);

uint8_t MFRC522_Check(uint8_t* id);
uint8_t MFRC522_Compare(uint8_t* CardID, uint8_t* CompareID);
void MFRC522_WriteRegister(uint8_t addr, uint8_t val);
uint8_t MFRC522_ReadRegister(uint8_t addr);
void MFRC522_SetBitMask(uint8_t reg, uint8_t mask);
void MFRC522_ClearBitMask(uint8_t reg, uint8_t mask);
uint8_t MFRC522_Request(uint8_t reqMode, uint8_t* TagType);
uint8_t MFRC522_ToCard(uint8_t command, uint8_t* sendData, uint8_t sendLen, uint8_t* backData, uint16_t* backLen);
uint8_t MFRC522_Anticoll(uint8_t* serNum);
void MFRC522_CalulateCRC(uint8_t* pIndata, uint8_t len, uint8_t* pOutData);
uint8_t MFRC522_SelectTag(uint8_t* serNum);
uint8_t MFRC522_Auth(uint8_t authMode, uint8_t BlockAddr, uint8_t* Sectorkey, uint8_t* serNum);
uint8_t MFRC522_Read(uint8_t blockAddr, uint8_t* recvData);
uint8_t MFRC522_Write(uint8_t blockAddr, uint8_t* writeData);
void MFRC522_Init(void);
void MFRC522_Reset(void);
void MFRC522_AntennaOn(void);
void MFRC522_AntennaOff(void);
void MFRC522_Halt(void);

void Runmotor(uint16_t duty1, GPIO_PinState Dir1,uint16_t duty2, GPIO_PinState Dir2);
void ConvertFloatToByteArray(float value, uint8_t bytes[4]);
void ConverUint16toArray(uint16_t value,uint8_t bytes[2]);
void ReceiveData(UART_HandleTypeDef *huart, uint8_t *pRX_Buffer, uint16_t Size);
void PID_line(float uk);
void Orient(void);
float PID_velocity(float setPoint, float current_value);
void PID_x(float x_ref, float x_measure);
void PID_y(float y_ref);

void UART_ReceiveData(UART_HandleTypeDef *huart, uint8_t *pRX_Buffer, uint16_t Size);
void UART_SendAGVInfo(UART_HandleTypeDef *huart);
void UART_SendLineTrackError(UART_HandleTypeDef *huart);
void UART_SendAck(UART_HandleTypeDef *huart, uint8_t ack, uint8_t functionCode);
void UART_SendToPIDGUI(UART_HandleTypeDef *huart);
uint16_t SumByteUInt16(uint16_t value);
uint16_t SumByteFloat(float value);

int8_t GetExitNodeFromRFID();
void GoAhead();
void TurnLeft();
void TurnRight();
void TurnBack();
void Backward();
void GetPathToRun();
void RunPath();
void StopAGV();
float LowPassFilter(float value, float alpha);
void RunState_a1();
void RunState_y1();
void RunState_a2();
void RunState_y2();
void RunState_x1();
void RunPickDrop(char pick_or_drop, char level);
/* USER CODE END PFP */

/* USER CODE BEGIN 0 */
#define AGV_ID 0x01
#define RX_MAX_SIZE 50
char initExitNode = 55;
char initDisToExitNode = 1;
char initOrient = 'N';
char status;
uint16_t exitNode;
char pre_orient,orient,DirToRun;
float distanceAGV = 0;
float vAGV;

const float Ts = 0.01;
uint32_t ENC1 = 0, ENC2 = 0, pre_ENC1 = 0, pre_ENC2 = 0;
uint32_t ENC3 = 0, pre_ENC3 = 0;
float rate_2 = 0,rate_1 = 0,rate = 0;
float x_current;
float y_current;
float x_setpoint = 0;
float y_setpoint = 0;
float a1, a2, y1 = 5, y2 = 10; // yi - cm
float _x1 = 0, x1 = 10, _x2 = 50, x2 = 60, _x3 = 100, x3 = 110; // xi - mm
char flagState = '0';
char flagPickCplt = 0;
char levelPD;
char PDtype;

uint8_t RX_Buffer[RX_MAX_SIZE];
uint16_t rxByteCount = 0;
uint8_t PathByteCount;
uint8_t Path[RX_MAX_SIZE];
char typeDataSend = 'A';
char flagSendData = 0;
uint16_t waitingTime; //ms
float setVelocity = 20; //cm/s

float yk_1 = 0;
float uk; //return value of PID_velocity
float ukLine; //return value of PID_line

uint8_t Path_Run[RX_MAX_SIZE];
uint8_t PathByteCount_Run;
char flagNewPath = 0;
char flagPathComplete = 1;

uint8_t PIDflag=0, OrientFlag=0;
uint8_t CardID[5];
uint8_t receivedCardID[5];
uint8_t flagCheckRFID = 0;

uint8_t phantram = 95;
	
uint8_t TagID[60][5] = {{0x99, 0x2E, 0x09, 0xF0, 0x4E},
												{0x69, 0x5C, 0x12, 0xEF, 0xC8},
												{0x49, 0x45, 0x09, 0xF0, 0xF5},
												{0x89, 0x64, 0x0C, 0xF0, 0x11},
												{0x89, 0x54, 0x12, 0xEF, 0x20},
												{0x99, 0x61, 0x12, 0xEF, 0x05},
												{0xB9, 0x57, 0x12, 0xEF, 0x13},
												{0xA9, 0x55, 0x12, 0xEF, 0x01},
												{0xA9, 0x56, 0x12, 0xEF, 0x02},
												{0x79, 0x0E, 0x0B, 0xF0, 0x8C},
												{0x69, 0x5B, 0x12, 0xEF, 0xCF},
												{0x59, 0x43, 0x09, 0xF0, 0xE3},
												{0xF9, 0x7B, 0x0A, 0xF0, 0x78},
												{0x89, 0x6F, 0x0A, 0xF0, 0x1C},
												{0x89, 0x6B, 0x0A, 0xF0, 0x18},
												{0x69, 0xE3, 0x09, 0xF0, 0x73},
												{0x69, 0xDF, 0x09, 0xF0, 0x4F},
												{0xE9, 0xCC, 0x09, 0xF0, 0xDC},
												{0xE9, 0xC8, 0x09, 0xF0, 0xD8},
												{0x49, 0x47, 0x09, 0xF0, 0xF7},
												{0x69, 0x41, 0x09, 0xF0, 0xD1},
												{0x29, 0x32, 0x09, 0xF0, 0xE2},
												{0x59, 0x1A, 0x07, 0xF0, 0xB4},
												{0x89, 0x17, 0x07, 0xF0, 0x69},
												{0xF9, 0x14, 0x07, 0xF0, 0x1A},
												{0x49, 0x9A, 0x06, 0xF0, 0x25},
												{0x79, 0x94, 0x06, 0xF0, 0x1B},
												{0x09, 0x99, 0x06, 0xF0, 0x66},
												{0xC9, 0x97, 0x06, 0xF0, 0xA8},
												{0x49, 0x96, 0x06, 0xF0, 0x29},
												{0x39, 0x92, 0x06, 0xF0, 0x5D},
												{0x59, 0x93, 0x06, 0xF0, 0x3C},
												{0xB9, 0x1B, 0x06, 0xF0, 0x54},
												{0xA9, 0x1B, 0x07, 0xF0, 0x45},
												{0x89, 0x95, 0x06, 0xF0, 0xEA},
												{0x49, 0x92, 0x06, 0xF0, 0x2D},
												{0xB9, 0x96, 0x06, 0xF0, 0xD9},
												{0x19, 0x99, 0x06, 0xF0, 0x76},
												{0xC9, 0x13, 0x07, 0xF0, 0x2D},
												{0x49, 0x16, 0x07, 0xF0, 0xA8},
												{0x99, 0x17, 0x07, 0xF0, 0x79},
												{0x09, 0x19, 0x07, 0xF0, 0xE7},
												{0xB9, 0x1B, 0x07, 0xF0, 0x55},
												{0x09, 0x99, 0x07, 0xF0, 0x67},
												{0x19, 0x9F, 0x07, 0xF0, 0x71},
												{0xE9, 0x9B, 0x07, 0xF0, 0x85},
												{0x19, 0xA2, 0x07, 0xF0, 0x4C},
												{0xA9, 0x1A, 0x08, 0xF0, 0x4B},
												{0x99, 0x1E, 0x08, 0xF0, 0x7F},
												{0xA9, 0x2E, 0x09, 0xF0, 0x7E},
												{0x09, 0x30, 0x08, 0xF0, 0xC1},
												{0x09, 0x2C, 0x08, 0xF0, 0xDD},
												{0x29, 0x62, 0x12, 0xEF, 0xB6},
												{0xA9, 0xAD, 0x08, 0xF0, 0xFC},
												{0xA9, 0xB1, 0x08, 0xF0, 0xE0},
												{0x19, 0xC0, 0x08, 0xF0, 0x21},
												{0x19, 0xC4, 0x08, 0xF0, 0x25},
												{0xC9, 0x61, 0x12, 0xEF, 0x55},
												{0x49, 0x69, 0x12, 0xEF, 0xDD},
												{0xF9, 0x98, 0x07, 0xF0, 0x96}}; 												
												
typedef struct{
	uint16_t Header;
	uint8_t FunctionCode; // 0x01
	uint8_t AGVID;
	uint8_t Status;
	uint16_t ExitNode;
	float DistanceToExitNode;
	uint8_t Orient;
	float Velocity;
	uint8_t Battery;
	uint16_t CheckSum;
	uint16_t EndOfFrame;
}__attribute__((packed)) SendAGVInfoStruct;

typedef struct{
	uint16_t Header;
	uint8_t FunctionCode; // 0x11
	uint8_t AGVID;
	float LineTrackError;
	uint16_t CheckSum;
	uint16_t EndOfFrame;
}__attribute__((packed)) SendLineTrackErrorStruct;

typedef struct{
	uint16_t Header;
	uint8_t FunctionCode; // 0x21, 0x02, 0x03 or 0x04
	uint8_t AGVID;
	uint8_t ACK; // 'Y' for ACK, 'N' for NACK
	uint16_t CheckSum;
	uint16_t EndOfFrame;
}__attribute__((packed)) SendAckStruct;

SendAGVInfoStruct SendAGVInfoFrame;
SendLineTrackErrorStruct SendLineTrackErrorFrame;
SendAckStruct SendAckFrame;

//----------------new-------------------
typedef struct{
	uint16_t Header;
	uint8_t FunctionCode; // 0x01
	uint8_t AGVID;
	float Velocity;
	float UdkVelocity;
  float LinePos;
  float UdkLinePos;
	uint16_t CheckSum;
	uint16_t EndOfFrame;
}__attribute__((packed)) SendToPIDGUIStruct;

SendToPIDGUIStruct SendToPIDGUIFrame;
//--------------------------------------

enum function_code {
	FUNC_REQ_AGV_INFO = 0xA0,
	FUNC_RESP_AGV_INFO = 0x01,
	FUNC_RESP_LINE_TRACK_ERR = 0x11,
	FUNC_RESP_ACK_AGV_INFO = 0x21,
	FUNC_WR_PATH = 0xA1,
	FUNC_RESP_ACK_PATH = 0x02,
	FUNC_WR_WAITING = 0xA2,
	FUNC_RESP_ACK_WAITING = 0x03,
	FUNC_WR_VELOCITY = 0xA3,
	FUNC_RESP_VELOCITY = 0x04,
	FUNC_REQ_AGV_INIT = 0xA4,
  FUNC_RESP_ACK_INIT = 0x05 
};
/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  *
  * @retval None
  */
int main(void)
{
  /* USER CODE BEGIN 1 */

  /* USER CODE END 1 */

  /* MCU Configuration----------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_DMA_Init();
  MX_SPI2_Init();
  MX_TIM1_Init();
  MX_TIM2_Init();
  MX_TIM3_Init();
  MX_TIM4_Init();
  MX_TIM5_Init();
  MX_TIM8_Init();
  MX_TIM9_Init();
  MX_USART2_UART_Init();

  /* Initialize interrupts */
  MX_NVIC_Init();
  /* USER CODE BEGIN 2 */
	
	/*--Init MFRC522--*/
	MFRC522_Init();
	
	/*--Enable channel 1 and 2 of TIM1 for PWM--*/
	HAL_TIM_PWM_Start(&htim1,TIM_CHANNEL_1);
	HAL_TIM_PWM_Start(&htim1,TIM_CHANNEL_2);
	HAL_TIM_PWM_Start(&htim1,TIM_CHANNEL_3);
	
	/*--Calibration sensor--*/
	for(int i = 0; i < 300; i++) QTRSensorsCalibrate();
	
	/*Khoi tao gia tri kenh Receive data*/
	HAL_UART_Receive_DMA(&huart2, (uint8_t *) RX_Buffer, RX_MAX_SIZE);
	
	// enable ngat TIM2, TIM9
	HAL_TIM_Base_Start_IT(&htim2);
	HAL_TIM_Base_Start_IT(&htim9);
	
		// Enable timer 3 va 4 doc 2 encoder
	HAL_TIM_Encoder_Start(&htim3,TIM_CHANNEL_1|TIM_CHANNEL_2);
	HAL_TIM_Encoder_Start(&htim4,TIM_CHANNEL_1|TIM_CHANNEL_2);
	HAL_TIM_Encoder_Start(&htim8, TIM_CHANNEL_1|TIM_CHANNEL_2);// Encoder GA25
	TIM8->CNT = 0;
	TIM3->CNT = 0;
	TIM4->CNT = 0;
	
	// khoi tao co bat bo PID, RFID, gia tri ban dau cua Orient Dir-Func
	PIDflag=0;     
	OrientFlag=1;
	exitNode = initExitNode;
	pre_orient='N';
	DirToRun='A';
	
  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
		if (MFRC522_Check(CardID) == MI_OK) flagCheckRFID = 1;
		else flagCheckRFID = 0;
		
		GetPathToRun();
		if (flagPathComplete == 0) RunPath();
		
		Orient();
  /* USER CODE END WHILE */

  /* USER CODE BEGIN 3 */
		UART_ReceiveData(&huart2, (uint8_t *) RX_Buffer, RX_MAX_SIZE);
		
		RunPickDrop(PDtype, levelPD);
		
//		TIM1->CCR3 = 200;
//		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_14, 1);
  }
  /* USER CODE END 3 */

}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{

  RCC_OscInitTypeDef RCC_OscInitStruct;
  RCC_ClkInitTypeDef RCC_ClkInitStruct;

    /**Configure the main internal regulator output voltage 
    */
  __HAL_RCC_PWR_CLK_ENABLE();

  __HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE1);

    /**Initializes the CPU, AHB and APB busses clocks 
    */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
  RCC_OscInitStruct.HSEState = RCC_HSE_ON;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
  RCC_OscInitStruct.PLL.PLLM = 4;
  RCC_OscInitStruct.PLL.PLLN = 84;
  RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV2;
  RCC_OscInitStruct.PLL.PLLQ = 4;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

    /**Initializes the CPU, AHB and APB busses clocks 
    */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

    /**Configure the Systick interrupt time 
    */
  HAL_SYSTICK_Config(HAL_RCC_GetHCLKFreq()/1000);

    /**Configure the Systick 
    */
  HAL_SYSTICK_CLKSourceConfig(SYSTICK_CLKSOURCE_HCLK);

  /* SysTick_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(SysTick_IRQn, 0, 0);
}

/**
  * @brief NVIC Configuration.
  * @retval None
  */
static void MX_NVIC_Init(void)
{
  /* TIM2_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(TIM2_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(TIM2_IRQn);
  /* TIM1_BRK_TIM9_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(TIM1_BRK_TIM9_IRQn, 1, 0);
  HAL_NVIC_EnableIRQ(TIM1_BRK_TIM9_IRQn);
}

/* SPI2 init function */
static void MX_SPI2_Init(void)
{

  /* SPI2 parameter configuration*/
  hspi2.Instance = SPI2;
  hspi2.Init.Mode = SPI_MODE_MASTER;
  hspi2.Init.Direction = SPI_DIRECTION_2LINES;
  hspi2.Init.DataSize = SPI_DATASIZE_8BIT;
  hspi2.Init.CLKPolarity = SPI_POLARITY_LOW;
  hspi2.Init.CLKPhase = SPI_PHASE_1EDGE;
  hspi2.Init.NSS = SPI_NSS_SOFT;
  hspi2.Init.BaudRatePrescaler = SPI_BAUDRATEPRESCALER_8;
  hspi2.Init.FirstBit = SPI_FIRSTBIT_MSB;
  hspi2.Init.TIMode = SPI_TIMODE_DISABLE;
  hspi2.Init.CRCCalculation = SPI_CRCCALCULATION_DISABLE;
  hspi2.Init.CRCPolynomial = 10;
  if (HAL_SPI_Init(&hspi2) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* TIM1 init function */
static void MX_TIM1_Init(void)
{

  TIM_ClockConfigTypeDef sClockSourceConfig;
  TIM_MasterConfigTypeDef sMasterConfig;
  TIM_OC_InitTypeDef sConfigOC;
  TIM_BreakDeadTimeConfigTypeDef sBreakDeadTimeConfig;

  htim1.Instance = TIM1;
  htim1.Init.Prescaler = 9;
  htim1.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim1.Init.Period = 999;
  htim1.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim1.Init.RepetitionCounter = 0;
  if (HAL_TIM_Base_Init(&htim1) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim1, &sClockSourceConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  if (HAL_TIM_PWM_Init(&htim1) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim1, &sMasterConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sConfigOC.OCMode = TIM_OCMODE_PWM1;
  sConfigOC.Pulse = 0;
  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
  sConfigOC.OCNPolarity = TIM_OCNPOLARITY_HIGH;
  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
  sConfigOC.OCIdleState = TIM_OCIDLESTATE_RESET;
  sConfigOC.OCNIdleState = TIM_OCNIDLESTATE_RESET;
  if (HAL_TIM_PWM_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  if (HAL_TIM_PWM_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_2) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  if (HAL_TIM_PWM_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_3) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sBreakDeadTimeConfig.OffStateRunMode = TIM_OSSR_DISABLE;
  sBreakDeadTimeConfig.OffStateIDLEMode = TIM_OSSI_DISABLE;
  sBreakDeadTimeConfig.LockLevel = TIM_LOCKLEVEL_OFF;
  sBreakDeadTimeConfig.DeadTime = 0;
  sBreakDeadTimeConfig.BreakState = TIM_BREAK_DISABLE;
  sBreakDeadTimeConfig.BreakPolarity = TIM_BREAKPOLARITY_HIGH;
  sBreakDeadTimeConfig.AutomaticOutput = TIM_AUTOMATICOUTPUT_DISABLE;
  if (HAL_TIMEx_ConfigBreakDeadTime(&htim1, &sBreakDeadTimeConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  HAL_TIM_MspPostInit(&htim1);

}

/* TIM2 init function */
static void MX_TIM2_Init(void)
{

  TIM_ClockConfigTypeDef sClockSourceConfig;
  TIM_MasterConfigTypeDef sMasterConfig;

  htim2.Instance = TIM2;
  htim2.Init.Prescaler = 83;
  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim2.Init.Period = 9999;
  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  if (HAL_TIM_Base_Init(&htim2) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim2, &sClockSourceConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* TIM3 init function */
static void MX_TIM3_Init(void)
{

  TIM_Encoder_InitTypeDef sConfig;
  TIM_MasterConfigTypeDef sMasterConfig;

  htim3.Instance = TIM3;
  htim3.Init.Prescaler = 0;
  htim3.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim3.Init.Period = 0xFFFF;
  htim3.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  sConfig.EncoderMode = TIM_ENCODERMODE_TI12;
  sConfig.IC1Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC1Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC1Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC1Filter = 0;
  sConfig.IC2Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC2Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC2Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC2Filter = 0;
  if (HAL_TIM_Encoder_Init(&htim3, &sConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim3, &sMasterConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* TIM4 init function */
static void MX_TIM4_Init(void)
{

  TIM_Encoder_InitTypeDef sConfig;
  TIM_MasterConfigTypeDef sMasterConfig;

  htim4.Instance = TIM4;
  htim4.Init.Prescaler = 0;
  htim4.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim4.Init.Period = 0xFFFF;
  htim4.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  sConfig.EncoderMode = TIM_ENCODERMODE_TI12;
  sConfig.IC1Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC1Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC1Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC1Filter = 0;
  sConfig.IC2Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC2Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC2Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC2Filter = 0;
  if (HAL_TIM_Encoder_Init(&htim4, &sConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim4, &sMasterConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* TIM5 init function */
static void MX_TIM5_Init(void)
{

  TIM_ClockConfigTypeDef sClockSourceConfig;
  TIM_MasterConfigTypeDef sMasterConfig;

  htim5.Instance = TIM5;
  htim5.Init.Prescaler = 167;
  htim5.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim5.Init.Period = 0xFFFF;
  htim5.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  if (HAL_TIM_Base_Init(&htim5) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim5, &sClockSourceConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim5, &sMasterConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* TIM8 init function */
static void MX_TIM8_Init(void)
{

  TIM_Encoder_InitTypeDef sConfig;
  TIM_MasterConfigTypeDef sMasterConfig;

  htim8.Instance = TIM8;
  htim8.Init.Prescaler = 0;
  htim8.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim8.Init.Period = 0xFFFF;
  htim8.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim8.Init.RepetitionCounter = 0;
  sConfig.EncoderMode = TIM_ENCODERMODE_TI12;
  sConfig.IC1Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC1Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC1Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC1Filter = 0;
  sConfig.IC2Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC2Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC2Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC2Filter = 0;
  if (HAL_TIM_Encoder_Init(&htim8, &sConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim8, &sMasterConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* TIM9 init function */
static void MX_TIM9_Init(void)
{

  TIM_ClockConfigTypeDef sClockSourceConfig;

  htim9.Instance = TIM9;
  htim9.Init.Prescaler = 79;
  htim9.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim9.Init.Period = 42000;
  htim9.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  if (HAL_TIM_Base_Init(&htim9) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim9, &sClockSourceConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* USART2 init function */
static void MX_USART2_UART_Init(void)
{

  huart2.Instance = USART2;
  huart2.Init.BaudRate = 115200;
  huart2.Init.WordLength = UART_WORDLENGTH_8B;
  huart2.Init.StopBits = UART_STOPBITS_1;
  huart2.Init.Parity = UART_PARITY_NONE;
  huart2.Init.Mode = UART_MODE_TX_RX;
  huart2.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart2.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart2) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/** 
  * Enable DMA controller clock
  */
static void MX_DMA_Init(void) 
{
  /* DMA controller clock enable */
  __HAL_RCC_DMA1_CLK_ENABLE();

  /* DMA interrupt init */
  /* DMA1_Stream5_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Stream5_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Stream5_IRQn);
  /* DMA1_Stream6_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Stream6_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Stream6_IRQn);

}

/** Configure pins as 
        * Analog 
        * Input 
        * Output
        * EVENT_OUT
        * EXTI
*/
static void MX_GPIO_Init(void)
{

  GPIO_InitTypeDef GPIO_InitStruct;

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOH_CLK_ENABLE();
  __HAL_RCC_GPIOC_CLK_ENABLE();
  __HAL_RCC_GPIOE_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOD_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(SS_GPIO_Port, SS_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOE, Dir_Right_Pin|Dir_Left_Pin|Dir_GA25_Pin|GPIO_PIN_0 
                          |GPIO_PIN_1, GPIO_PIN_RESET);

  /*Configure GPIO pin : SS_Pin */
  GPIO_InitStruct.Pin = SS_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(SS_GPIO_Port, &GPIO_InitStruct);

  /*Configure GPIO pins : Dir_Right_Pin Dir_Left_Pin Dir_GA25_Pin PE0 
                           PE1 */
  GPIO_InitStruct.Pin = Dir_Right_Pin|Dir_Left_Pin|Dir_GA25_Pin|GPIO_PIN_0 
                          |GPIO_PIN_1;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOE, &GPIO_InitStruct);

}

/* USER CODE BEGIN 4 */
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{		
	if(htim->Instance == TIM9) // 40ms
	{
		// Send AGV info
		flagSendData = flagSendData ? 0 : 1;
		if (flagSendData == 1) UART_SendAGVInfo(&huart2);
		else UART_SendLineTrackError(&huart2);
//		UART_SendToPIDGUI(&huart2);
		
	}			
	if(htim->Instance == TIM2) // 10ms
	{
		position = QTRSensorsReadLine(sensorValues);
		
		ENC1 = TIM3->CNT;
		ENC2 = TIM4->CNT;
		ENC3 = TIM8->CNT;
		
		uint32_t deltaENC1 = ENC1 - pre_ENC1;
		uint32_t deltaENC2 = ENC2 - pre_ENC2;
		
		pre_ENC1 = ENC1;
		pre_ENC2 = ENC2;
		
		rate_1 = deltaENC1*100.0f*20.4204/(363*4);	// Toc do cm/s
		rate_2 = deltaENC2*100.0f*20.4204/(363*4);
		
		// Follow line & speed control
		if (rate_1 > 100) rate_1 = 100;
		if (rate_2 > 100) rate_2 = 100;
		rate = (rate_1 + rate_2)/2;
		vAGV = LowPassFilter(rate, 0.96);
		
		uk = PID_velocity(setVelocity, vAGV);
		PID_line(uk);
		
		// update distance of AGV
		if(PIDflag == 1)
		{
			if (distanceAGV > 100) distanceAGV = 0;
			distanceAGV += 20.4204*deltaENC1/4/363;
		}
		
		// Lift Control
		x_current = ENC3*8.0f/(374*4); //mm
		PID_x(x_setpoint, x_current);
		PID_y(y_setpoint);
		
	}
}
//----------QTR Read Function-------------------------------
// Reads the raw values of the sensors
void QTRSensorsRead(uint16_t *_sensorValues)
{
	uint16_t __sensorValues[sensorCount];
	for(int i = 0; i < sensorCount; i++)
		__sensorValues[i] = maxValue; // maxValue = 1000
	// make sensor line an output
	QTRPins_OUTPUT_Mode();
	// drive sensor line high- bat cac chan cam bien len 1
	HAL_GPIO_WritePin(GPIOA, GPIO_PIN_5|GPIO_PIN_1|GPIO_PIN_2|GPIO_PIN_3 
                          |GPIO_PIN_4, GPIO_PIN_SET);
	// charge lines for 10 us-thay doi line trong 10us
	// HAL_Delay(1);
//	// disable interrupts so we can switch all the pins as close to the same
//  // time as possible
//	HAL_TIM_Base_Stop_IT(&htim5);
	// record start time before the first sensor is switched to input
  // (similarly, time is checked before the first sensor is read in the
  // loop below)
	uint16_t time = 0;
	// make sensor line an input (should also ensure pull-up is disabled)
	QTRPins_INPUT_Mode();
	TIM5->CNT = 0;
	HAL_TIM_Base_Start(&htim5);
	while(time < maxValue)
	{
		// disable interrupts so we can read all the pins as close to the same
    // time as possible
		// HAL_TIM_Base_Stop(&htim5);
		time = TIM5->CNT;
		for (int i = 0; i < sensorCount; i++)
    {
			if ((HAL_GPIO_ReadPin(GPIOA, sensorPins[i]) == GPIO_PIN_RESET) && (time < __sensorValues[i]))
			{
				// record the first time the line reads low
				__sensorValues[i] = time;
				_sensorValues[i] = __sensorValues[i];
			}
    }
		// HAL_TIM_Base_Start(&htim5);
	}
	HAL_TIM_Base_Stop(&htim5);
}
//-----------QTR Calibrate Function-------------------------
// Find the min & max values during calibration and store in min/maxCalibValues
void QTRSensorsCalibrate(void)
{
	uint16_t _sensorValues[sensorCount];
  static uint16_t maxSensorValues[sensorCount];
  static uint16_t minSensorValues[sensorCount];
	if(calibInitialized == '0')
	{
		// Initialize the max and min calibrated values to values that
    // will cause the first reading to update them.
		for(int i = 0; i < sensorCount; i++)
    {
      minCalibValues[i] = maxValue;
      maxCalibValues[i] = 0;
    }
		calibInitialized = '1';
	}
	
	for(int j = 0; j < 10; j++)
  {
    QTRSensorsRead(_sensorValues);
    for(int i = 0; i < sensorCount; i++)
    {
      // set the max we found THIS time
      if((j == 0) || (_sensorValues[i] > maxSensorValues[i]))
				maxSensorValues[i] = _sensorValues[i];
      // set the min we found THIS time
      if((j == 0) || (_sensorValues[i] < minSensorValues[i]))
        minSensorValues[i] = _sensorValues[i];
    }
  }
	// record the min and max calibration values
  for(int i = 0; i < sensorCount; i++)
  {
    if(maxSensorValues[i] > maxCalibValues[i])
			maxCalibValues[i] = maxSensorValues[i];
    if(minSensorValues[i] < minCalibValues[i])
      minCalibValues[i] = minSensorValues[i];
  }
}
//-----------QTR Read Calibrated Values Function------------
//Reads the sensors and provides calibrated values between 0 and 500.
void QTRSensorsReadCalibrated(uint16_t *_sensorValues)
{
	uint16_t __sensorValue[sensorCount];
	// if not calibrated, do nothing
	if(calibInitialized == '0') return;
	// read the needed values
	QTRSensorsRead(__sensorValue);
	for(int i = 0; i < sensorCount; i++)
	{
		uint16_t calibmin, calibmax;
		calibmin = minCalibValues[i];
		calibmax = maxCalibValues[i];
		uint16_t denominator = calibmax - calibmin;//denta
		
    int16_t value = 0;
		if(denominator != 0)
      value = (((int32_t)__sensorValue[i]) - calibmin) * 500 / denominator;
		
    if(value < 0) {value = 0;}
    else if(value > 500) {value = 500;}

    _sensorValues[i] = value;
	}	
}
//-----------QTR Read Line Function-------------------------
// Note: This project only use 3 middle-sensor,
// so 'sensorCount-2' and _sensorValues[i+1] will be used
uint16_t QTRSensorsReadLine(uint16_t *_sensorValues)
{
	char onLine = '0';
  uint32_t avg = 0; // this is for the weighted total
  uint16_t sum = 0; // this is for the denominator, which is <= 64000
	static uint16_t _lastPosition = 0;
	QTRSensorsReadCalibrated(_sensorValues);
	for(int i = 0; i < sensorCount-2; i++)
	{
		uint16_t value = _sensorValues[i+1];
		// keep track of whether we see the line at all
		if(value > 100) {onLine = '1';}
		// only average in values that are above a noise threshold
		if(value > 50)
		{
			avg += (uint32_t)value * (i * 1000);
      sum += value;
		}
	}
	if(onLine == '0')
	{
		// If it last read to the left of center, return 0.
    if (_lastPosition < (sensorCount-2 - 1) * 1000 / 2)
      return 0;
    // If it last read to the right of center, return the max.
    else
      return (sensorCount-2 - 1) * 1000;
	}
	_lastPosition = avg / sum;
  return _lastPosition;
}
//-----------QTR Pins Mode Function-------------------------
void QTRPins_OUTPUT_Mode(void)
{
  GPIO_InitTypeDef GPIO_InitStruct;

  /*Configure GPIO pins : PA5 PA1 PA2 PA3 PA4 */
  GPIO_InitStruct.Pin = GPIO_PIN_5|GPIO_PIN_1|GPIO_PIN_2|GPIO_PIN_3 
                          |GPIO_PIN_4;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);
}
void QTRPins_INPUT_Mode(void)
{
  GPIO_InitTypeDef GPIO_InitStruct;

  /*Configure GPIO pins : PA5 PA1 PA2 PA3 PA4 */
  GPIO_InitStruct.Pin = GPIO_PIN_5|GPIO_PIN_1|GPIO_PIN_2|GPIO_PIN_3 
                          |GPIO_PIN_4;
  GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);
}

// Ham dieu khien motor
void Runmotor(uint16_t duty1, GPIO_PinState Dir1,uint16_t duty2, GPIO_PinState Dir2)
{
	if (duty1 > 1000 ) duty1 = 1000; // prevent the motor from going beyond max speed
  if (duty2 > 1000 ) duty2 = 1000; // prevent the motor from going beyond max speed
  if (duty1 < 0) duty1 = 0; // keep the motor speed positive
  if (duty2 < 0) duty2 = 0; // keep the motor speed positive 
	if (Dir1 == GPIO_PIN_RESET) TIM1->CCR1=duty1;
	else TIM1->CCR1=(TIM1->ARR)-duty1;
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_10,Dir1);
	if (Dir2 == GPIO_PIN_RESET) TIM1->CCR2 = duty2;
	else TIM1->CCR2=(TIM1->ARR)-duty2;
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_12, Dir2);
}
// Tinh toan gia tri PID
float pre_ekVeloc = 0,ekVeloc = 0;
float sum_ekVeloc = 0;
float outputVeloc = 0;
float ukVeloc;

float KpVeloc = 3;
float KiVeloc = 4;
float KdVeloc = 0.015;
float PID_velocity(float setPoint, float current_value)
{
	if (PIDflag == 0) return 0;
	
	ekVeloc = setPoint - current_value;
	sum_ekVeloc = sum_ekVeloc + Ts*ekVeloc;
	ukVeloc = KpVeloc*(ekVeloc) + KiVeloc*sum_ekVeloc+ (KdVeloc/Ts)*(ekVeloc - pre_ekVeloc);
	pre_ekVeloc = ekVeloc;
	
	float outputPWM = ukVeloc*10;
	if (outputPWM > 1000) outputPWM = 1000;
	else if (outputPWM < 0) outputPWM = 0;

	return outputPWM;
 }

float error;
float lastError = 0;
float sum_error = 0;
float Kp = 0.5;
float Ki = 0.0005;
float Kd = 0.05;
void PID_line(float udk)
{
	if (PIDflag == 0) return;
	
	DirToRun='A';
	
	error = 1000 - position;
	sum_error = sum_error + Ts*error;
	float delta = Kp*error + Ki*sum_error + (Kd/Ts)*(error - lastError);
	lastError = error;
	
	if (delta > udk) delta = udk;
	else if (delta < -udk) delta = -udk;
	ukLine = delta; // for send to PID GUI test
	int rightMotorSpeed = udk + delta;
	int leftMotorSpeed = udk - delta;
	
	Runmotor(rightMotorSpeed ,0,leftMotorSpeed,0);
	
//	Runmotor(udk, 0, udk, 0);
	
	status='R';	
	HAL_GPIO_WritePin(GPIOD,GPIO_PIN_14,1);
}

// Huong di hien tai cua AGV
void Orient(void)
{
	if (OrientFlag==1)
	{
	switch (pre_orient)
	{
		// if pre-orient ='E'
		case 'E':
		{
					switch(DirToRun)
					{
								case 'A':
								{																															
										orient='E';
										pre_orient='E';
										OrientFlag=0;									
										break;
								}
								case 'L':
								{
										orient='N';
										pre_orient='N';
										OrientFlag=0;
										break;
								}
								case 'R':
								{
										orient='S';
										pre_orient='S';
										OrientFlag=0;
										break;
								}
								case 'B':
								{
										pre_orient='W';
										orient='W';
										OrientFlag=0;
										break;
								}
					}
								break;
		}
		// if pre-orient ='W'
		case 'W':
		{
			switch(DirToRun)
			{
				case 'A':
				{
					orient='W';
					pre_orient='W';
					OrientFlag=0;
					break;
				}
				case 'L':
				{
					orient='S';
					pre_orient='S';
					OrientFlag=0;
					break;
				}
				case 'R':
				{
					orient='N';
					pre_orient='N';
					OrientFlag=0;
					break;
				}
				case 'B':
				{
					orient='E';
					pre_orient='E';
					OrientFlag=0;
				  break;
				}
		  }
					break;							
		}
	// if pre-orient ='S'	
		case 'S':
		{
			switch(DirToRun)
			{
				case 'A':
				{
					orient='S';
					pre_orient='S';
					OrientFlag=0;
					break;
				}
				case 'L':
				{
					orient='E';
					pre_orient='E';
					OrientFlag=0;
					break;
				}
				case 'R':
				{
					orient='W';
					pre_orient='W';
					OrientFlag=0;
					break;
				}
				case 'B':
				{
					orient='N';
					pre_orient='N';
					OrientFlag=0;
					break;
				}
			}
				break;							
		}			
	// if pre-orient ='S'	
		case 'N':
		{
					switch(DirToRun)
					{
						case 'A':
						{
							orient='N';
							pre_orient='N';
							OrientFlag=0;
							break;
						}
						case 'L':
						{																					
							orient='W';
							pre_orient='W';	
							OrientFlag=0;																			
							break;
						}
						case 'R':
						{
							orient='E';
							pre_orient='E';
							OrientFlag=0;
							break;
						}
						case 'B':
						{
							orient='S';
							pre_orient='S';
							OrientFlag=0;
							break;
						}
					}
						  break;							
			}						
	}		
}
	else return;
	}
//Tinh toan khoang cach so voi exitnode		
/**
  * @brief  Receives an amount of data. 
  * @param  huart pointer to a UART_HandleTypeDef structure that contains
  *                the configuration information for the specified UART module.
  * @param  *pRX_Buffer Pointer to RX_Buffer
  * @param  Size Amount of RX_Buffer
  */
void UART_ReceiveData(UART_HandleTypeDef *huart, uint8_t *pRX_Buffer, uint16_t Size)
{
	rxByteCount = Size - __HAL_DMA_GET_COUNTER(huart -> hdmarx);
	
	// waitting for 4 bytes to check header, function code, agvID
	if (rxByteCount < 4) return;
	
	// check header, agvID and get function code if header is detected
	for (int i = 0; i < rxByteCount - 4; i++)
	{
		// check header, agvID
		if (RX_Buffer[i] != 0xAA || RX_Buffer[i + 1] != 0xFF) continue;
		if (RX_Buffer[i + 3] != AGV_ID) continue;
		
		uint16_t startIndex = i; // start index of header in RX_Buffer
		uint8_t functionCode = RX_Buffer[i + 2];
		
		if (functionCode == FUNC_WR_PATH) // receive path of AGV
		{
			// waitting for receive enough frame data of this function code
			if (rxByteCount - startIndex < 5) return;
			uint16_t pathByteCount = RX_Buffer[startIndex + 4];
			uint16_t frameByteCount = 5 + pathByteCount + 4;
			if (rxByteCount - startIndex < frameByteCount) return;
			
			// get frame data to an array
			uint8_t arrFrame[frameByteCount];
			for (int i = 0; i < frameByteCount; i++)
				arrFrame[i] = RX_Buffer[i + startIndex];
			
			// check sum
			uint16_t crc = 0;
			for(int i = 0; i < frameByteCount - 4; i++) crc += arrFrame[i];
			if((crc&0xff)!= arrFrame[frameByteCount - 4] || ((crc>>8)&0xff)!= arrFrame[frameByteCount - 3])
			{
				// send NACK
				UART_SendAck(huart, 'N', FUNC_RESP_ACK_PATH);
				return;
			}
			
			// get path byte count and path data
			PathByteCount = arrFrame[4];
			for (int i = 0; i < PathByteCount; i++) Path[i] = arrFrame[5 + i];
			
			//set flag new path
			flagNewPath = 1;
			
			// send ACK
			UART_SendAck(huart, 'Y', FUNC_RESP_ACK_PATH);
		}
		else if (functionCode == FUNC_REQ_AGV_INFO) // receive AGV-info request
		{
			// waitting for receive enough frame data of this function code
			uint16_t frameByteCount = 9;
			if (rxByteCount - startIndex < frameByteCount) return;
			
			// get frame data to an array
			uint8_t arrFrame[frameByteCount];
			for (int i = 0; i < frameByteCount; i++)
				arrFrame[i] = RX_Buffer[i + startIndex];
			
			// check sum
			uint16_t crc = 0;
			for(int i = 0; i < frameByteCount - 4; i++) crc += arrFrame[i];
			if((crc&0xff)!= arrFrame[frameByteCount - 4] || ((crc>>8)&0xff)!= arrFrame[frameByteCount - 3])
			{
				
				// send NACK
				UART_SendAck(huart, 'N', FUNC_RESP_ACK_AGV_INFO);
				return;
			}
			
			// get type of info to send
			typeDataSend = arrFrame[4];
			
			// send ACK
			UART_SendAck(huart, 'Y', FUNC_RESP_ACK_AGV_INFO);
		}
		else if (functionCode == FUNC_WR_WAITING) // receive waiting information
		{
			// waitting for receive enough frame data of this function code
			uint16_t frameByteCount = 10;
			if (rxByteCount - startIndex < frameByteCount) return;
			
			// get frame data to an array
			uint8_t arrFrame[frameByteCount];
			for (int i = 0; i < frameByteCount; i++)
				arrFrame[i] = RX_Buffer[i + startIndex];
			
			// check sum
			uint16_t crc = 0;
			for(int i = 0; i < frameByteCount - 4; i++) crc += arrFrame[i];
			if((crc&0xff)!= arrFrame[frameByteCount - 4] || ((crc>>8)&0xff)!= arrFrame[frameByteCount - 3])
			{
				// send NACK
				UART_SendAck(huart, 'N', FUNC_RESP_ACK_WAITING);
				return;
			}
			
			// get type of info to send
			waitingTime = (arrFrame[5]<<8) | arrFrame[4];
			
			// send ACK
			UART_SendAck(huart, 'Y', FUNC_RESP_ACK_WAITING);
		}
		else if (functionCode == FUNC_WR_VELOCITY)
		{
			// waitting for receive enough frame data of this function code
			uint16_t frameByteCount = 12;
			if (rxByteCount - startIndex < frameByteCount) return;
			
			// get frame data to an array
			uint8_t arrFrame[frameByteCount];
			for (int i = 0; i < frameByteCount; i++)
				arrFrame[i] = RX_Buffer[i + startIndex];
			
			// check sum
			uint16_t crc = 0;
			for(int i = 0; i < frameByteCount - 4; i++) crc += arrFrame[i];
			if((crc&0xff)!= arrFrame[frameByteCount - 4] || ((crc>>8)&0xff)!= arrFrame[frameByteCount - 3])
			{
				// send NACK
				UART_SendAck(huart, 'N', FUNC_RESP_VELOCITY);
				return;
			}
			
			// get velocity
			memcpy(&setVelocity, &arrFrame[4], 4);
			
			// send ACK
			UART_SendAck(huart, 'Y', FUNC_RESP_VELOCITY);
		}
		else if (functionCode == FUNC_REQ_AGV_INIT)
		{
			// waitting for receive enough frame data of this function code
			uint16_t frameByteCount = 9;
			if (rxByteCount - startIndex < frameByteCount) return;
			
			// get frame data to an array
			uint8_t arrFrame[frameByteCount];
			for (int i = 0; i < frameByteCount; i++)
				arrFrame[i] = RX_Buffer[i + startIndex];
			
			// check sum
			uint16_t crc = 0;
			for(int i = 0; i < frameByteCount - 4; i++) crc += arrFrame[i];
			if((crc&0xff)!= arrFrame[frameByteCount - 4] || ((crc>>8)&0xff)!= arrFrame[frameByteCount - 3])
			{
				// send NACK
				UART_SendAck(huart, 'N', FUNC_RESP_ACK_INIT);
				return;
			}
			
			// set agv info type = 'A'
			typeDataSend = 'A';
			
			// send ACK
			UART_SendAck(huart, 'Y', FUNC_RESP_ACK_INIT);
		}
		////-------------------------new-----------------------------
		else if (functionCode == 0xAC) //set PID Velocity
		{
			// waitting for receive enough frame data of this function code
			uint16_t frameByteCount = 24;
			if (rxByteCount - startIndex < frameByteCount) return;
			
			// get frame data to an array
			uint8_t arrFrame[frameByteCount];
			for (int i = 0; i < frameByteCount; i++)
				arrFrame[i] = RX_Buffer[i + startIndex];
			
			// check sum
			uint16_t crc = 0;
			for(int i = 0; i < frameByteCount - 4; i++) crc += arrFrame[i];
			if((crc&0xff)!= arrFrame[frameByteCount - 4] || ((crc>>8)&0xff)!= arrFrame[frameByteCount - 3])
				return;
			
			memcpy(&setVelocity, &arrFrame[16], 4);
			memcpy(&KpVeloc, &arrFrame[4], 4);
			memcpy(&KiVeloc, &arrFrame[8], 4);
			memcpy(&KdVeloc, &arrFrame[12], 4);
		}
		else if (functionCode == 0xAD) //set PID Velocity
		{
			// waitting for receive enough frame data of this function code
			uint16_t frameByteCount = 20;
			if (rxByteCount - startIndex < frameByteCount) return;
			
			// get frame data to an array
			uint8_t arrFrame[frameByteCount];
			for (int i = 0; i < frameByteCount; i++)
				arrFrame[i] = RX_Buffer[i + startIndex];
			
			// check sum
			uint16_t crc = 0;
			for(int i = 0; i < frameByteCount - 4; i++) crc += arrFrame[i];
			if((crc&0xff)!= arrFrame[frameByteCount - 4] || ((crc>>8)&0xff)!= arrFrame[frameByteCount - 3])
				return;
			
			memcpy(&Kp, &arrFrame[4], 4);
			memcpy(&Ki, &arrFrame[8], 4);
			memcpy(&Kd, &arrFrame[12], 4);
		}
		
		/** If function code isn't match any data packet struct OR finish get data,
			* reload for next reception (to correct index RX_Buffer)
			*/
		__HAL_DMA_DISABLE(huart -> hdmarx); // example: huart -> hdmarx = huart2.hdmarx
		HAL_UART_Receive_DMA(huart, pRX_Buffer, Size);
		rxByteCount = 0;
		
		break;
	}	
}

void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
  /* Prevent unused argument(s) compilation warning */
  UNUSED(huart);
  
	// Reload for next reception (to correct index RX_Buffer)
	__HAL_DMA_DISABLE(huart -> hdmarx); // example: huart -> hdmarx = huart2.hdmarx
	HAL_UART_Receive_DMA(huart, (uint8_t *) RX_Buffer, RX_MAX_SIZE);
	rxByteCount = 0;
}

void UART_SendAGVInfo(UART_HandleTypeDef *huart)
{
	// assign fixed variable
	SendAGVInfoFrame.Header = 0xFFAA;
	SendAGVInfoFrame.FunctionCode = FUNC_RESP_AGV_INFO;
	SendAGVInfoFrame.AGVID = AGV_ID;
	SendAGVInfoFrame.EndOfFrame = 0x0D0A;
	//assign status,orient,battery,velocity,exitnode,distancetoexitnode
	SendAGVInfoFrame.Status=status;
	SendAGVInfoFrame.Orient=orient;
	SendAGVInfoFrame.Battery=phantram;
	SendAGVInfoFrame.ExitNode=exitNode;
	SendAGVInfoFrame.DistanceToExitNode=distanceAGV;
	SendAGVInfoFrame.Velocity = vAGV;
	
	// calculate check sum
	uint16_t crc = 0;
	crc += 0xFF + 0xAA;
	crc += SendAGVInfoFrame.FunctionCode;
	crc += SendAGVInfoFrame.AGVID;
	crc += SendAGVInfoFrame.Status;
	crc += SendAGVInfoFrame.Orient;
	crc += SendAGVInfoFrame.Battery;
	crc += SumByteUInt16(SendAGVInfoFrame.ExitNode);
	crc += SumByteFloat(SendAGVInfoFrame.DistanceToExitNode);
	crc += SumByteFloat(SendAGVInfoFrame.Velocity);
	SendAGVInfoFrame.CheckSum = crc;
	
	// send data
	HAL_UART_Transmit_DMA(huart, (uint8_t *) &SendAGVInfoFrame, sizeof(SendAGVInfoFrame));
}

void UART_SendLineTrackError(UART_HandleTypeDef *huart)
{
	if (typeDataSend == 'A') return;
	// assign fixed variable
	SendLineTrackErrorFrame.Header = 0xFFAA;
	SendLineTrackErrorFrame.FunctionCode = FUNC_RESP_LINE_TRACK_ERR;
	SendLineTrackErrorFrame.AGVID = AGV_ID;
	SendLineTrackErrorFrame.EndOfFrame = 0x0D0A;
	
	// assign  line track error
	SendLineTrackErrorFrame.LineTrackError = error;
	
	// calculate check sum
	uint16_t crc = 0;
	crc += 0xFF + 0xAA;
	crc += SendLineTrackErrorFrame.FunctionCode;
	crc += SendLineTrackErrorFrame.AGVID;
	crc += SumByteFloat(SendLineTrackErrorFrame.LineTrackError);
	SendLineTrackErrorFrame.CheckSum = crc;
	
	// send data
	HAL_UART_Transmit_DMA(huart, (uint8_t *) &SendLineTrackErrorFrame, sizeof(SendLineTrackErrorFrame));
}

void UART_SendAck(UART_HandleTypeDef *huart, uint8_t ack, uint8_t functionCode)
{
	SendAckFrame.Header = 0xFFAA;
	SendAckFrame.FunctionCode = functionCode;
	SendAckFrame.AGVID = AGV_ID;
	SendAckFrame.ACK = ack;
	SendAckFrame.EndOfFrame = 0x0D0A;
	
	// calculate check sum
	uint16_t crc = 0;
	crc += 0xFF + 0xAA;
	crc += SendAckFrame.FunctionCode;
	crc += SendAckFrame.AGVID;
	crc += SendAckFrame.ACK;
	SendAckFrame.CheckSum = crc;
	
	// send data
	HAL_UART_Transmit_DMA(huart, (uint8_t *) &SendAckFrame, sizeof(SendAckFrame));
}

//---------------------new------------------------------
void UART_SendToPIDGUI(UART_HandleTypeDef *huart)
{
	// assign fixed variable
	SendToPIDGUIFrame.Header = 0xFFAA;
	SendToPIDGUIFrame.FunctionCode = 0xAB;
	SendToPIDGUIFrame.AGVID = AGV_ID;
	SendToPIDGUIFrame.EndOfFrame = 0x0D0A;
	SendToPIDGUIFrame.Velocity = vAGV;
	SendToPIDGUIFrame.UdkVelocity = uk;
	SendToPIDGUIFrame.LinePos = (float)position;
	SendToPIDGUIFrame.UdkLinePos = ukLine;
	
	// calculate check sum
	uint16_t crc = 0;
	crc += 0xFF + 0xAA;
	crc += SendToPIDGUIFrame.FunctionCode;
	crc += SendToPIDGUIFrame.AGVID;
	crc += SumByteFloat(SendToPIDGUIFrame.Velocity);
	crc += SumByteFloat(SendToPIDGUIFrame.UdkVelocity);
	crc += SumByteFloat(SendToPIDGUIFrame.LinePos);
	crc += SumByteFloat(SendToPIDGUIFrame.UdkLinePos);
	SendToPIDGUIFrame.CheckSum = crc;
	
	// send data
	HAL_UART_Transmit_DMA(huart, (uint8_t *) &SendToPIDGUIFrame, sizeof(SendToPIDGUIFrame));
}
//-----------------------------------------------------------------------
/* Calculate the sum of 4 bytes of float number */
uint16_t SumByteFloat(float value)
{
	union{
		float floatNum;
		uint8_t bytes[4];
	} converter;
	
	uint16_t sumByte = 0;
	converter.floatNum = value;
	for (int i = 0; i < 4; i++) sumByte += converter.bytes[i];
	return sumByte;
}

/* Calculate the sum of 2 bytes of uint16_t number */
uint16_t SumByteUInt16(uint16_t value)
{
	union{
		uint16_t uint16Num;
		uint8_t bytes[2];
	} converter;
	
	uint16_t sumByte = 0;
	converter.uint16Num = value;
	for (int i = 0; i < 2; i++) sumByte += converter.bytes[i];
	return sumByte;
}

void GoAhead()
{
	DirToRun = 'A';
	OrientFlag = 1;				
	PIDflag = 1;
}

void TurnLeft()
{
	PIDflag = 0;
	DirToRun = 'L';
	OrientFlag = 1;
	Runmotor(430,0,350,1);				
	HAL_Delay(200); // delay cho ra khoi line
	while((position<650)||(position>1350)) Runmotor(430,0,350,1);	
	PIDflag = 1;
}

void TurnRight()
{
	PIDflag = 0;
	DirToRun = 'R';
	OrientFlag = 1;
	Runmotor(350,1,430,0);				
	HAL_Delay(200); // delay cho ra khoi line
	while((position<650)||(position>1350)) Runmotor(350,1,430,0);
	PIDflag = 1;
}
void TurnBack()
{
	PIDflag = 0;
	DirToRun='B';
	OrientFlag=1;
	Runmotor(375,0,375,1);				
	HAL_Delay(700); // delay cho ra khoi line
	while((position<650)||(position>1350)) Runmotor(375,0,375,1);
	PIDflag=1;
}
void GetPathToRun()
{
	if (flagNewPath == 1 && flagPathComplete == 1)
	{
		for (int i = 0; i < PathByteCount; i++) Path_Run[i] = Path[i];
		PathByteCount_Run = PathByteCount;
		flagPathComplete = 0;
		flagNewPath = 0;
	}
}

void StopAGV()
{
	PIDflag = 0;
	Runmotor(0,0,0,0);
	status = 'S';
	HAL_GPIO_WritePin(GPIOD,GPIO_PIN_14,1);
}

void RunPath()
{
	 int8_t thisNode = GetExitNodeFromRFID();
	 
	 // tra ve dung node, cap nhat node hien tai
	 exitNode = thisNode;
	 
	 if (exitNode == initExitNode)
	 {
		 if (Path_Run[0] == 'P' && flagState == '0' && flagPickCplt == 0)
		 {
			 status = 'P';
			 flagState = '1';
			 PDtype = 'P';
			 levelPD = Path_Run[1];
		 }
		 if (Path_Run[0] == 'P' && flagPickCplt == 0) return;
		 switch (Path_Run[2])
		 {
			 case 'A': GoAhead(); break;
			 case 'B': TurnBack(); Path_Run[2] = 'A'; break;
		 }
	 }
	 else
	 {
		 // tim index cua node hien tai trong path_run
		 int indexCurrentNode;
		 for (int i = 2; i < PathByteCount_Run - 2; i++)
		 {
			 if (exitNode == Path_Run[i])
			 {
				 indexCurrentNode = i;
				 break;
			 }
		 }
		 // lay huong chay
		 uint8_t direction = Path_Run[indexCurrentNode + 1];
		 switch (direction)
		 {
			 case 'A': GoAhead(); break;
			 case 'L': TurnLeft(); Path_Run[indexCurrentNode + 1] = 'A'; break;
			 case 'R': TurnRight(); Path_Run[indexCurrentNode + 1] = 'A'; break;
			 case 'B': TurnBack(); Path_Run[indexCurrentNode + 1] = 'A'; break;
			 case 'G': 
				 if (Path_Run[PathByteCount_Run - 2] == 'N')
				 {
					 StopAGV();
					 flagPathComplete = 1;
					 initExitNode = exitNode;
				 }
				 if (Path_Run[PathByteCount_Run - 2] == 'D' && flagState == '0')
				 {
					 StopAGV();
					 status = 'D';
					 flagState = '1';
					 PDtype = 'D';
					 levelPD = Path_Run[PathByteCount_Run - 1];
				 }
				 break;
		 }	 
	 }
}
 
int8_t GetExitNodeFromRFID()
{
	if (flagCheckRFID == 0) return exitNode; // khong co the rfid
	
	distanceAGV = 0;
	
	for (int i = 0; i < 60; i++)
	{
		uint8_t compareID[5];
		for (int j = 0; j < 5; j++) compareID[j] = TagID[i][j];
		if (MFRC522_Compare(CardID, compareID) == MI_OK)
		{
			return i;
		}
	}
	return exitNode;
}

float LowPassFilter(float xk, float alpha)
{
	float yk = alpha*yk_1 + (1.0f - alpha)*xk;
	yk_1 = yk;
	return yk;
}

float pre_ex = 0, ex = 0;
float sum_ex = 0;
float Kp_x = 2.5;
float Ki_x = 0;
char PIDxFlag = 0;
void PID_x(float x_ref, float x_measure)
{
	if (PIDxFlag == 0) return;
	
	ex = x_ref - x_measure;
	sum_ex = sum_ex + Ts*ex;
	float ux = Kp_x*ex+ Ki_x*sum_ex;	
	pre_ex = ex;

	int outpwm = ux*100;
	if (outpwm > 350) outpwm = 350;
	if (outpwm < -350) outpwm = -350;
	
	if (outpwm > 0)
	{
		TIM1->CCR3 = outpwm;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_14, 0);
		
	}
	else if (outpwm < 0)
	{
		TIM1->CCR3 =  1000 + outpwm;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_14, 1);
	}
	else if (outpwm == 0 ) 
	{
		TIM1->CCR3 = 0;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_14, 0);	
	}							
}

float pre_ey = 0, ey = 0;
float sum_ey = 0;
float Kp_y = 13;
float Ki_y = 0.5;
float Kd_y = 0.02;
char PIDyFlag = 0;
void PID_y(float y_ref)
{
	if (PIDyFlag == 0) return;
	
	y_current = 20.4204f*(ENC1 - 300.0)/4/363;
	
	ey = y_ref - y_current;
	sum_ey = sum_ey + Ts*ey;
	float uy = Kp_y*ey+ Ki_y*sum_ey + (Kd_y/Ts)*(ey - pre_ey);	
	pre_ey = ey;

	int outpwm = uy*10;
	if (outpwm > 300) outpwm = 300;
	if (outpwm < -300) outpwm = -300;
	
	if (outpwm > 0)
	{
		TIM1->CCR1 = outpwm + 35;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_10, 0);
		TIM1->CCR2 = outpwm;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_12, 0);
		
	}
	else if (outpwm < 0)
	{
		TIM1->CCR1 = TIM1->ARR + outpwm;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_10, 1);
		TIM1->CCR2 = TIM1->ARR + outpwm + 20;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_12, 1);
	}
	else if (outpwm == 0)
	{
		TIM1->CCR1 = 0;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_10, 0);
		TIM1->CCR2 = 0;
		HAL_GPIO_WritePin(GPIOE,GPIO_PIN_12, 0);		
	}							
}

void RunState_a1()
{
	if (flagState != '1') return;
	
	x_setpoint = a1;
	PIDxFlag = 1;
	if (x_current < x_setpoint  - 1.5 || x_current > x_setpoint  + 1.5) return;
	PIDxFlag = 0;
	flagState = '2'; // next state
	TIM1->CCR3 = 0;
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_14, 0);
	
	// reset encoder for PIDy
	TIM3->CNT = 300;
	TIM4->CNT = 300;
	ENC1 = 300;
	ENC2 = 300;
}

void RunState_y2()
{
	if (flagState != '2') return;
	
	y_setpoint = y2;
	PIDyFlag = 1;
	if (y_current < y_setpoint  - 1|| y_current > y_setpoint  + 1) return;
	PIDyFlag = 0;
	flagState = '3';
	TIM1->CCR1 = 0;
	TIM1->CCR2 = 0;
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_10, 0);
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_12, 0);
}

void RunState_a2()
{
	if (flagState != '3') return;
	
	x_setpoint = a2;
	PIDxFlag = 1;
	if (x_current < x_setpoint  - 1.5 || x_current > x_setpoint  + 1.5) return;
	PIDxFlag = 0;
	flagState = '4';
	TIM1->CCR3 = 0;
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_14, 0);
}

void RunState_y1()
{
	if (flagState != '4') return;
	
	y_setpoint = y1;
	PIDyFlag = 1;
	if (y_current < y_setpoint  - 1 || y_current > y_setpoint  + 1) return;
	PIDyFlag = 0;
	flagState = '5';
	TIM1->CCR1 = 0;
	TIM1->CCR2 = 0;
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_10, 0);
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_12, 0);
}

void RunState_x1()
{
	if (flagState != '5') return;
	
	x_setpoint = _x1;
	PIDxFlag = 1;
	if (x_current < x_setpoint  - 1.5 || x_current > x_setpoint  + 1.5) return;
	PIDxFlag = 0;
	flagState = '0';
	TIM1->CCR3 = 0;
	HAL_GPIO_WritePin(GPIOE,GPIO_PIN_14, 0);
	
	if (exitNode != initExitNode)
	{
		flagPathComplete = 1;
		initExitNode = exitNode;
		flagPickCplt = 0;
	}
	else flagPickCplt = 1;
}

void RunPickDrop(char pick_or_drop, char level)
{
	if (pick_or_drop == 'P')
	{
		switch (level)
		{
			case 1: a1 = _x1; a2 = x1; break;
			case 2: a1 = _x2; a2 = x2; break;
			case 3: a1 = _x3; a2 = x3; break;
		}
		RunState_a1();
		RunState_y2();
		RunState_a2();
		RunState_y1();
		RunState_x1();
	}
	else if (pick_or_drop == 'D')
	{
		switch (level)
		{
			case 1: a1 = x1; a2 = _x1; break;
			case 2: a1 = x2; a2 = _x2; break;
			case 3: a1 = x3; a2 = _x3; break;
		}
		RunState_a1();
		RunState_y2();
		RunState_a2();
		RunState_y1();
		RunState_x1();
	}
}
/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @param  file: The file name as string.
  * @param  line: The line in file as a number.
  * @retval None
  */
void _Error_Handler(char *file, int line)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  while(1)
  {
  }
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t* file, uint32_t line)
{ 
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     tex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/**
  * @}
  */

/**
  * @}
  */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
