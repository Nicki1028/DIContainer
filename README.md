# 自製 DI Container

## 簡介

Nicki DI是一個以學習與實踐為目的所開發的輕量級Dependency Injection容器，實作了.NET DI 的核心概念，包括服務註冊（Service Registration）、依賴解析（Dependency Resolution）、
生命週期管理（Lifetime Management）以及建構式注入（Constructor Injection）。
除了基本的 DI 功能外，也提供Attribute自動註冊、MVP架構整合與Component Factory等擴充能力。

## 架構圖
![Architecture](images/structure.png)

## 功能特色
- 建構式依賴注入（Constructor Injection）
- Singleton / Transient 生命週期管理
- Attribute 自動註冊
- `IEnumerable<T>` 多重服務解析
- MVP 架構整合
- 自訂 Presenter Factory
- 自訂 Component Factory
