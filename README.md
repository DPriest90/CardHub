# CardHub – Yu-Gi-Oh Booster Pack Viewer

CardHub is a Windows Forms application that scrapes and displays Yu-Gi-Oh! booster pack contents from [Konami’s official card database](https://www.db.yugioh-card.com). It provides a searchable, sortable interface for exploring the cards within each pack, designed for archivists, developers, and fans who value clarity and control.

## 🧠 Core Features

- 🔍 **Booster Pack Selection**  
  Browse and select from all available booster packs listed on Konami’s site.

- 📋 **Card Display Grid**  
  Upon selection, all cards from the chosen pack are displayed in a sortable, filterable `AdvancedDataGridView`.

- ⚙️ **Data Scraping & Caching**  
  - On first run, CardHub scrapes the full booster pack list and card data (approx. 5–10 minutes).
  - All data is saved locally in the app’s folder for instant load times on subsequent runs.

- 📁 **Local Data Files**  
  - `BoosterPackCardData.json`: Maps each booster pack to its card list.
  - `PackNameUrlMap.json`: Maps each booster pack to its original Konami URL.
  - `BoosterPackList.txt`: Plain-text list of all booster pack names.

## 🚀 First-Time Setup

1. Launch the app.
2. Initial scrape will begin automatically (5–10 minutes depending on connection).
3. Once complete, all data is cached locally.
4. Future launches will load instantly using cached data.

## ✅ Requirements

- Windows OS
- .NET Framework (WinForms compatible)

## 📄 License

This application and dataset are provided as-is for educational and archival purposes. No affiliation with Konami or official Yu-Gi-Oh! entities.

---

