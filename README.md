# StockAnalyzer – Stock Analysis & Pattern Recognition App

**Candlestick charting and pattern recognition in C# Windows Forms**

---

## 📖 Description
StockAnalyzer is a Windows Forms application that visualizes stock price data in candlestick format with volume overlays. It extends beyond basic charting by introducing **SmartCandlestick objects** and an **extensible pattern recognition system** that detects 14+ candlestick patterns. Each stock opens in its own form, supports real‑time updates, and uses annotations to highlight detected patterns.

---

## ✨ Features
- **Candlestick & Volume Charts**
  - Displays OHLC (Open, High, Low, Close) with color‑coded candles (green for up, red for down).
  - Volume shown as a column plot beneath price data.
- **Dynamic Filtering & Multi‑Stock**
  - Date range filters with preset start date.
  - Load and compare multiple stocks simultaneously, each in its own form.
- **SmartCandlestick Class**
  - Properties for Range, BodyRange, Upper/Lower Tails, TopPrice, and BottomPrice.
  - Classification of bullish, bearish, doji, hammer, marubozu, and more.
- **Pattern Recognition System**
  - Abstract `Recognizer` base class with specialized `Recognizer_xxx` implementations.
  - Supports **single‑, double‑, and triple‑candlestick patterns** (14+ total).
  - Detected patterns annotated directly on charts (RectangleAnnotation / ArrowAnnotation).
- **Normalized Charts**
  - Y‑axis scaling automatically adjusted to display data in full dynamic range.
- **OOP & Extensibility**
  - Separation of concerns between data loading, candlestick modeling, and recognition logic.

---

## 🧰 Tech Stack
- **Language:** C#
- **Framework:** .NET Windows Forms
- **Design:** Object‑Oriented Programming, Abstract Classes, Inheritance
- **Data:** CSV stock data (Daily, Weekly, Monthly) from Yahoo Finance

---

## 🗺️ Architecture Overview
```
[CSV Loader] ──► [Candlestick List] ──► [SmartCandlestick]
                                       │
                                       └─► [Recognizer_xxx]
                                                │
                                                ▼
                                          Pattern Detections ──► [Annotations on Chart]
```

---

## ⚙️ Getting Started

### 1) Prerequisites
- Visual Studio 2022+
- .NET Framework (Windows Forms)
- Stock data CSVs from Yahoo Finance placed in **Stock Data/** folder

### 2) Clone Repository
```bash
git clone https://github.com/<your-username>/StockAnalyzer.git
cd StockAnalyzer
```

### 3) Open & Build
- Open the solution in Visual Studio
- Build → Clean Solution → Rebuild Solution

### 4) Run
- Run the project
- Use **OpenFileDialog** to select a stock CSV (Daily, Weekly, Monthly)
- Adjust date pickers and ComboBox to filter and detect patterns

---

## 📊 Example Patterns Supported
- **Single Candle:** Bullish, Bearish, Neutral, Doji, Hammer, Dragonfly Doji, Gravestone Doji, Marubozu
- **Two Candle:** Bullish Engulfing, Bearish Engulfing, Bullish Harami, Bearish Harami
- **Three Candle:** Peaks, Valleys

---

## 🧪 Testing & Validation
- Verified that candlesticks display in correct temporal order.
- Confirmed green/red coloring matches open/close values.
- Cross‑checked detected patterns with known examples.
- Normalized charts tested across multiple timeframes (Daily, Weekly, Monthly).

---

## 🚀 Future Improvements
- Multi‑pattern highlighting on the same chart.
- Export charts as images or PDF reports.
- Live data integration with APIs (instead of static CSVs).

---

## 🤝 Contributing
PRs are welcome! Please follow code conventions:
- Comment all functions with `///` summaries.
- Use consistent naming convention: `controlType_name`.
- Ensure Designer‑generated code is not manually edited.

---

