using com.amtec.forms;
using com.amtec.model;
using com.itac.mes.imsapi.client.dotnet;
using com.itac.mes.imsapi.domain.container;
using System;


namespace com.amtec.action
{
    public class GetCurrentWorkorder
    {
        private static IMSApiDotNet imsapi = IMSApiDotNet.loadLibrary();
        private IMSApiSessionContextStruct sessionContext;
        private ProductionForm view;
        private int error;

        public GetCurrentWorkorder(IMSApiSessionContextStruct sessionContext, ProductionForm view)
        {
            this.sessionContext = sessionContext;
            this.view = view;
        }

        public GetStationSettingModel GetCurrentWorkorderResultCall(string station)
        {

            GetStationSettingModel stationSetting = new GetStationSettingModel();
            String[] stationSettingResultKey = new String[] { "BOM_VERSION", "WORKORDER_NUMBER", "PART_NUMBER", "WORKORDER_STATE", "PROCESS_VERSION", "PROCESS_LAYER", "ATTRIBUTE_1", "QUANTITY", "PART_DESC" };
            String[] stationSettingResultValues;
            LogHelper.Info("begin api trGetStationSetting (Station number:" + station + ")");
            //station = "XS1D-S010-01";
            error = imsapi.trGetStationSetting(sessionContext, station, stationSettingResultKey, out stationSettingResultValues);
            LogHelper.Info("end api trGetStationSetting (result code = " + error + ")");
            if (error != 0)
            {
                LogHelper.Error(" trGetStationSetting " + error);
                return null;
            }
            LogHelper.Info(" trGetStationSetting " + error);
            stationSetting.bomVersion = stationSettingResultValues[0];
            stationSetting.workorderNumber = stationSettingResultValues[1];
            stationSetting.partNumber = stationSettingResultValues[2];
            stationSetting.workorderState = stationSettingResultValues[3];
            stationSetting.processVersion = int.Parse(stationSettingResultValues[4]);
            stationSetting.processLayer = int.Parse(stationSettingResultValues[5]);
            stationSetting.attribute1 = stationSettingResultValues[6];
            stationSetting.QuantityMO = int.Parse(stationSettingResultValues[7]);
            stationSetting.partdesc = stationSettingResultValues[8];
            return stationSetting;
        }

        public string[] GetMachineStructrueData(string lineNumber,string station)
        {
            int error = 0;
            KeyValue[] machineAssetStructureFilter = new KeyValue[] { new KeyValue("DISSOLVING_LEVEL", "1"), new KeyValue("FUNC_MODE", "1"), new KeyValue("LINE_NUMBER", lineNumber) };
            string[] machineAssetStructureResultKeys = new string[] { "STATION_NUMBER", "STATION_DESC", "LINE_DESC" };
            string[] machineAssetStructureValues = new string[] { };
            error = imsapi.mdataGetMachineAssetStructure(sessionContext, station, machineAssetStructureFilter, machineAssetStructureResultKeys, out machineAssetStructureValues);
            LogHelper.Info("Api mdataGetMachineAssetStructure error=" + error);
            return machineAssetStructureValues;
        }

        public string[] GetProcessLayerByWO(string workorder, string stationNumber)
        {
            KeyValue[] workplanFilter = new KeyValue[] { new KeyValue("FUNC_MODE", "0"), new KeyValue("WORKORDER_NUMBER", workorder) };
            string[] workplanDataResultKeys = new string[] { "PROCESS_LAYER", "ERP_GROUP_NUMBER" };
            string[] workplanDataResultValues = new string[] { };
            int errorWP = imsapi.mdataGetWorkplanData(sessionContext, stationNumber, workplanFilter, workplanDataResultKeys, out workplanDataResultValues);
            LogHelper.Info("Api mdataGetWorkplanData: work order number =" + workorder + ", station number =" + stationNumber + ", result code =" + errorWP);
            return workplanDataResultValues;
        }
        public string[] GetstationdescByWO(string workorder, string stationNumber)
        {
            KeyValue[] workplanFilter = new KeyValue[] { new KeyValue("FUNC_MODE", "0"), new KeyValue("WORKORDER_NUMBER", workorder), new KeyValue("STATION_BASED_WORKSTEPS", "1") };
            string[] workplanDataResultKeys = new string[] { "STATION_NUMBER", "STATION_DESC", "PROCESS_LAYER" };
            string[] workplanDataResultValues = new string[] { };
            int errorWP = imsapi.mdataGetWorkplanData(sessionContext, stationNumber, workplanFilter, workplanDataResultKeys, out workplanDataResultValues);
            LogHelper.Info("Api mdataGetWorkplanData: work order number =" + workorder + ", station number =" + stationNumber + ", result code =" + errorWP);
            return workplanDataResultValues;
        }
    }
}
