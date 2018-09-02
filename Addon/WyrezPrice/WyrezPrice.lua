local lineAdded = false

local function padRight (s, l, c)
    local res = s .. strrep(c or ' ', l - #s)
    return res, res ~= s
end

local function formatPrice(price)
    local gold, silverCopper = strsplit(".", price)
    if silverCopper == nil then silverCopper = "0" end
    silverCopper = padRight(silverCopper, 4, "0")
    local silver = strsub(silverCopper, 1, 2)
    local copper = strsub(silverCopper, 3, 4)

    return "|cFFFFFFFF" .. padRight(gold, 1, "0") .. "|cFFFFD700g|cFFFFFFFF " .. silver .. "|cFFC0C0C0s|cFFFFFFFF " .. copper .. "|cFFeda55fc|r"
end

local function OnTooltipSetItem(tooltip, ...)
	if not lineAdded then
        local _, itemLink = GameTooltip:GetItem()
	    local _, itemId = strsplit(":", string.match(itemLink, "item[%-?%d:]+"))

        local itemData = goblineer_data[itemId]
        if itemData ~= nil then
            local itemCount = goblineer_itemCount 

            local minPrice, mvPrice

            if(IsShiftKeyDown() and itemCount ~= nil) then
                minPrice = tonumber(itemData["MIN"]) * itemCount
                mvPrice = tonumber(itemData["marketvalue"]) * itemCount
            else 
                minPrice = tonumber(itemData["MIN"])
                mvPrice = tonumber(itemData["marketvalue"])                    
            end

            tooltip:AddLine("   ")
            tooltip:AddLine("Wyrez-Price:", 1, 1, 0, true)

            if(IsShiftKeyDown() and itemCount ~= 0) then
                tooltip:AddDoubleLine("    Min Price |cFFC0C0C0x" .. itemCount .. "|r", formatPrice(minPrice), 130/255, 130/255, 250/255)
                tooltip:AddDoubleLine("    Marketvalue |cFFC0C0C0x" .. itemCount .. "|r", formatPrice(mvPrice), 130/255, 130/255, 250/255)
            else
                tooltip:AddDoubleLine("    Min Price", formatPrice(minPrice), 130/255, 130/255, 250/255)
                tooltip:AddDoubleLine("    Marketvalue", formatPrice(mvPrice), 130/255, 130/255, 250/255)
            end

            tooltip:AddDoubleLine("    Quantity", itemData["quantity"], 130/255, 130/255, 250/255, 1, 1, 1)
            tooltip:AddLine("   ")
        end
        lineAdded = true
	end
end

local function OnTooltipCleared(tooltip, ...)
   lineAdded = false
end

hooksecurefunc (GameTooltip, "SetBagItem",
    function(tip, whichbag, whichslot)
        goblineer_texture, goblineer_itemCount, goblineer_locked, goblineer_quality, goblineer_readable = GetContainerItemInfo(whichbag, whichslot)
    end
)
 
GameTooltip:HookScript("OnTooltipSetItem", OnTooltipSetItem)
GameTooltip:HookScript("OnTooltipCleared", OnTooltipCleared)