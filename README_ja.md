# target_volume_extractor_esapi_v15_5

ターゲットの体積を取得するためのStandalone executableです。

ESAPI v15.5で作成されています。

<span style="color:#ff0000;">本ソフトウェアの多言語への翻訳は途中です。</span>



## 使用方法

1. ターゲットの体積を取得したいプランの、患者IDとプラン名が記載されたcsvファイルを作成する ([target_volume_extractor_test.csv
](https://github.com/akiaji-k/target_volume_extractor_esapi_v15_5/blob/main/target_volume_extractor_test.csv))。

   ```
   # id, plan_name
   00001111,P221125_STx
   00001111,P221125_STx_m
   
   ```

2. 手順1で作成したcsvファイルを引数として、standalone executableを実行する(csvファイルを実行ファイルにドラッグアンドドロップするなど)。

3. 抽出されたターゲット体積がcsvファイルとして出力されます([volume_extractor_outputs.csv
](https://github.com/akiaji-k/target_volume_extractor_esapi_v15_5/blob/main/volume_extractor_outputs.csv))。

   ```
   # #1: ID, #2: Plan ID, #3: X_len_mm, #4: Y_len_mm, #5: Z_len_mm, #6: Volume_cc
   00001111,P221125_STx,36.08685303,36.22903442,35.97485352,24.81413687
   00001111,P221125_STx_m,98.82525635,54.77008057,132.4902344,703.7093405
   
   ```



## LICENSE

MIT ライセンスで公開されています。

本ソフトウェアで発生したことについて、いかなる責任も負いません。

詳細は [LICENSE](https://github.com/akiaji-k/4DCT_namer/blob/main/LICENSE) をご確認ください。
