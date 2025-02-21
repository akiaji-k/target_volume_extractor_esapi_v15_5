# target_volume_extractor_esapi_v15_5

ESAPI Standalone Executable to extract target volumes.

Created with ESAPI v15.5.

<span style="color:#ff0000;">This is in the process of i8n.</span>



## How to use

1. Create a csv file with the patient IDs and plan names which you want to obtain target volumes ([target_volume_extractor_test.csv
](https://github.com/akiaji-k/target_volume_extractor_esapi_v15_5/blob/main/target_volume_extractor_test.csv).

   ```
   # id, plan_name
   00001111,P221125_STx
   00001111,P221125_STx_m
   
   ```

2. Execute the standalone executable with the csv file created in step 1 as an argument (e.g., drag and drop the csv file onto the executable).

3. The extracted target volume is output as a csv file ([volume_extractor_outputs.csv
](https://github.com/akiaji-k/target_volume_extractor_esapi_v15_5/blob/main/volume_extractor_outputs.csv).

   ```
   # #1: ID, #2: Plan ID, #3: X_len_mm, #4: Y_len_mm, #5: Z_len_mm, #6: Volume_cc
   00001111,P221125_STx,36.08685303,36.22903442,35.97485352,24.81413687
   00001111,P221125_STx_m,98.82525635,54.77008057,132.4902344,703.7093405
   
   ```
   
   

## LICENSE

Released under the MIT license.

No responsibility is assumed for anything that occurs with this software.

See [LICENSE](https://github.com/akiaji-k/plan_checker_gui_esapi_v15_5/blob/main/LICENSE) for further details.
