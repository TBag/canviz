Param(
 [string]$subscriptionId = "3b7912c3-ad06-426e-8627-41912372774b",
 [string]$resourceGroup = "ContosoInsurance-Phase3-Update",
 [string]$logicAppName = "customer-submit-claim",
 [string]$triggerName = "manual"
)

Function Get-Folder()
{
    
    
    [System.Reflection.Assembly]::LoadWithPartialName("System.windows.forms")

    $foldername = New-Object System.Windows.Forms.FolderBrowserDialog
    $foldername.rootfolder = "MyComputer"

    if($foldername.ShowDialog() -eq "OK")
    {
        $folder += $foldername.SelectedPath
    }
    return $folder
}

Login-AzureRmAccount -SubscriptionId $subscriptionId
$folder = Get-Folder
$filePath =  $folder.GetValue(1) + "\" + $logicAppName + ".swagger.json"
$swagger = Invoke-AzureRmResourceAction -ResourceType "Microsoft.Logic/workflows" -ResourceGroupName $resourceGroup -ResourceName $logicAppName -Action listSwagger -ApiVersion "2016-06-01" -Force | ConvertTo-Json -Depth 99 | Out-File $filePath
$triggerResourceName = $logicAppName + "/$triggerName"
$callbackUrl = Invoke-AzureRmResourceAction -ResourceType "Microsoft.Logic/workflows/triggers" -ResourceGroupName $resourceGroup -ResourceName $triggerResourceName -Action listCallbackURL -ApiVersion "2016-06-01" -Force
Write-Output "`n`n`nWrote file to $filePath`n`n"
$URL = [System.Web.HttpUtility]::ParseQueryString($callbackUrl.value)
Write-Output $callbackUrl.method  $callbackUrl.value
$sp = $URL.GetValues('sp')
$sv = $URL.GetValues('sv')
$sig = $URL.GetValues('sig')
Write-Output "`n`n sp = $sp `n`n sv =  $sv `n`n sig = $sig `n`n api-version = 2016-06-01"