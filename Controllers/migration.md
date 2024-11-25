## Migration plan

																																																		 
|Before | After |																																														  
|--------|-------|																																														   
| **City**| |																																															   |
| GET.Entry.City															| 	GET.Cities.One																											   |
| GET.View.City																| 	GET.Cities.Many																											   |
|-DELETE.City																|   DELETE.City																												   |
| PULL.City																	|   PUT.City																												   |
| PUT.City																	| 	POST.City																												   |
| 																			| 																														   |
|  **Employee** | |																	 																														   |
| GET.Entry.Employee														|  GET.Employees.One																										   |
| GET.View.Employees														|  GET.Employees.Many																										   |
| GET.View.EmployeesBy														|  GET.Employees.ManyByPositions																							   |
|-DELETE.Employee															|  DELETE.Employee																											   |
| PULL.Employee																|  PUT.Employee																												   |
| PUT.Employee																|  POST.Employee																											   |
|																			| 																														   |
|  **ComponentCalculation** | 													| 																														   |
| GET.View.ComponentCalculationsBy(string)									|  GET.ComponentCalculations.ManyByKind																						   |
| GET.View.ComponentCalculationsBy(string, int)								|  GET.ComponentCalculations.ManyByKindAndEmployee																			   |
| GET.View.ComponentCalculationsBy(int)										|  GET.ComponentCalculations.ManyByProcurement																				   |
| GET.View.ComponentCalculationsBy(List<int?>, List< string>)				|  GET.ComponentCalculations.ManyByProcurementsAndStatuses																	   |
| GET.View.ComponentCalculationsBy(int, List< string>)						|  GET.ComponentCalculations.ManyByOneProcurementAndStatuses																   |
| GET.View.Aggregate.ComponentClculationCountBy								|  GET.ComponentCalculations.CountByProcurement																				   |
| DELETE.ComponentCalculation(ComponentCalculation)							|  DELETE.ComponentCalculations.One																							   |
| DELETE.ComponentCalculation(int)											|  DELETE.ComponentCalculations.ManyByParentName																			   |
| PULL.ComponentCalculation													|  PUT.ComponentCalculation																									   |
| PUT.ComponentCalculation													|  POST.ComponentCalculation																								   |
|																			| 																														   |
|																			| 																														   |
|  **Platform** | 																| 																													   |
| GET.Entry.Platform(string, string)										|  GET.Platforms.OneByNameAndAddress																						   |
| GET.Entry.Platform(string)												|  GET.Platforms.OneByAddress																								   |
| PUT.Platform																|  POST.Platform																											   |
|																			| 																														   |
|  **Comment** | 																		| 																														   |
| GET.View.CommentsBy(int)													|  GET.Comments(int)																										   |
| GET.View.CommentsBy(int, bool)											|  GET.Comments(int, bool=true)																								   |
| PUT.Comment																|  POST.Comment																												   |
|																			| 																														   |
|  **ComponentState** | 																| 																														   |
| GET.View.ComponentStates													|  GET.ComponentStates																										   |
|-DELETE.ComponentStates													|  DELETE.ComponentStates																									   |
| PUT.ComponentStates														|  POST.ComponentStates																										   |
| PULL.ComponentStates														|  PUT.ComponentStates																										   |
|																			| 																														   |
|  **ComponentHeaderType** | 															| 																														   |
| GET.View.ComponentHeaderTypes												|  GET.ComponentHeaderTypes																									   |
|-DELETE.ComponentHeaderType												|  DELETE.ComponentHeaderType																								   |
| PUT.ComponentHeaderType													|  POST.ComponentHeaderType																									   |
| PULL.ComponentHeaderType													|  PUT.ComponentHeaderType  																								   |
| 																			| 																														   |
|  **ComponentType** | 																| 																														   |
| GET.View.ComponentTypes													|  GET.ComponentHeaderTypes																									   |
|-DELETE.ComponentType														|  DELETE.ComponentType																										   |
| PUT.ComponentType															|  POST.ComponentType																										   |
| PULL.ComponentType														|  PUT.ComponentType																										   |
|																			| 																														   |
|  **Document** | 																| 																														   |
| GET.View.Document															|  GET.Documents																											   |
|-DELETE.Document															|  DELETE.Document																											   |
| PUT.Document																|  POST.Document																											   |
| PULL.Document																|  PUT.Document																												   |
|																			| 																														   |
|  **Law** | 																		| 																														   |
| GET.Entry.Law																|  GET.Laws.one																												   |
| Get.View.Laws																|  Get.Laws.Many																											   |
| PUT.Law																	|  POST.Law																													   |
|																			| 																														   |
|  **Tag** | 																		| 																														   |
| GET.View.Tags																|  GET.Tags																													   |
|-DELETE.Tag																|  DELETE.Tag																												   |
| PULL.Tag																	|  PUT.Tag																													   |
| PUT.Tag																	|  POST.Tag																													   |
|																			| 																														   |
|  **TagException** | 															| 																														   |
| GET.View.TagExceptions													| > GET.TagExceptions																										   |
|-DELETE.TagException														|  DELETE.TagException																										   |
| PULL.TagException															|  PUT.TagException																											   |
| PUT.TagException															|  POSt.TagException																										   |
|																			| 																														   |
|  **Manufacturer** | 															| 																														   |
| GET.Entry.Manufacturer													|  GET.Manufacturers.One																									   |
| GET.View.Manufacturer														|  GET.Manufacturers.Many																									   |
|-DELETE.Manufacturer														|  DELETE.Manufacturer																										   |
| PULL.Manufacturer															|  PUT.Manufacturer																											   |
| PUT.Manufacturer															|  POST.Manufacturer																										   |
|																			| 																														   |
|  **ManufacturerCountry** | 														| 																														   |
| GET.View.ManufacturerCountries											|  GET.ManufacturerCountries																								   |
|-DELETE.ManufacturerCountry												|  DELETE.ManufacturerCountry																								   |
| PULL.ManufacturerCountry													|  PUT.ManufacturerCountry																									   |
| PUT.ManufacturerCountry													|  POST.ManufacturerCountry																									   |
|																			| 																														   |
|  **Region** | 																	| 																														   |
| GET.Entry.Region															|  GET.Regions.One																											   |
| GET.View.Regions															|  GET.Regions.Many																											   |
|-DELETE.Region																|  DELETE.Region																											   |
| PULL.Region																|  PUT.Region																												   |
| PUT.Region																|  POST.Region																												   |
| 																			| 																														   |
|  **Organization** | 															| 																														   |
| GET.Entry.Organization(string)											|  GET.Organizations.OneByName																								   |
| GET.Entry.Organization(string, string)									|  GET.Organizations.OneByNameAndAddress																					   |
| GET.View.Organizations													|  GET.Organizations.Many																									   |
| PUT.Organization															|  POST.Organization																										   |
|																			| 																														   |
|  **Seller** | 																		| 																														   |
| GET.View.Sellers															|  GET.Sellers																												   |
|-DELETE.Seller																|  DELETE.Seller																											   |
| PULL.Seller																|  PUT.Seller																												   |
| PUT.Seller																|  POST.Seller																												   |
|																			| 																														   |
|  **Position** | 																	| 																														   |
| GET.View.Positions														|  GET.Positions																											   |
|-DELETE.Position															|  DELETE.Position																											   |
| PULL.Position																|  PUT.Position																												   |
| PUT.Position																|  POST.Position																											   |
|																			| 																														   |
|  **History** | 																	| 																														   |
| GET.View.Histories														|  GET.Histories.Many																										   |
| GET.View.HistoriesBy														|  GET.Histories.ManyByProcurement																							   |
| GET.Aggregate.CountNewStatusByProcurementId								|  GET.Histories.CountNewStatusByProcurement																				   |
| PUT.History																|  POST.History																												   |
| GET.View.HistoryGroupBy													|  GET.Histories.GroupByProcurementState for one: (string, histories) for all 7 states: (histories)							   |
|																			| 																														   |
|  **LegalEntity** | 																| 																														   |
| GET.View.LegalEntities													|  GET.LegalEntities																										   |
|-DELETE.LegalEntity														|  DELETE.LegalEntity																										   |
| PULL.LegalEntity															|  PUT.LegalEntity																											   |
| PUT.LegalEntity															|  POST.LegalEntity																											   |
|																			| 																														   |
|  **Minopttorg** | 																| 																														   |
| GET.View.Minopttorgs														|  GET.Minopttorgs																											   |
|-DELETE.Minopttorg															|  DELETE.Minopttorg																										   |
| PULL.Minopttorg															|  PUT.Minopttorg																											   |
| PUT.Minopttorg															|  POST.Minopttorg																											   |
|																			| 																														   |
|  **PredefinedComponent** | 															| 																														   |
| GET.View.PredefinedComponents												|  GET.PredefinedComponents																									   |
|-DELETE.PredefinedComponent												|  DELETE.PredefinedComponent																								   |
| PULL.PredefinedComponent													|  PUT.PredefinedComponent																									   |
| PUT.PredefinedComponent													|  POST.PredefinedComponent																									   |
|																			| 																														   |
|  **Preference** | 																	| 																														   |
| GET.View.Preferences														|  GET.Preferences																											   |
|-DELETE.Preference															|  DELETE.Preference																										   |
| PULL.Preference															|  PUT.Preference																											   |
| PUT.Preference															|  POST.Preference																											   |
|																			| 																														   |
|  **Method** |																		| 																														   |
| GET.Entry.Method															|  GET.Methods.One																											   |
| GET.View.Methods															|  GET.Methods.Many																											   |
| PUT.Methos																|  POST.Method																												   |
|																			| 																														   |
|  **TimeZone** | 																| 																														   |
| GET.Entry.TimeZone														|  GET.TimeZones.One	_dependent_: PUT.ProcurementSource																	   |
| GET.View.TimeZones														|  GET.TimeZones.Many																										   |
| PUT.TimeZone																|  POST.TimeZone		_dependent_: PUT.ProcurementSource																	   |
|																			| 																														   |
|  **Application** | 																	| 																														   |
| GET.View.ApplicationsBy													|  GET.Applications.Many																									   |
| GET.Aggregate.NumberOfApplication											|  GET.Applications.Number																									   |
| GET.Aggregate.CountOfApplication											|  GET.Applications.Count																									   |
|																			| 																														   |
|  **Display** | 																		| 																														   |
| GET.Aggregate.MaxDisplayId												|  GET.DisplayId.First																										   |
| GET.Aggregate.DisplayId													|  GET.DisplayId.Max																										   |
| | |
|  **Procurement** | 																| 																														   |
| PULL.Procurement															|  PUT.Procurement.One																										   |
| PULL.ProcurementSource													|  PUT.Procurement.Source																									   |
| PULL.Procurement_ProcurementState											|  PUT.Procurement.ProcurementState																							   |
| PUT.Procurement															|  POST.Procurements.One																									   |
| PUT.ProcurementSource														|  POST.Procurements.Source																									   |
|-DELETE.Procurement														|  DELETE.Procurement																										   |
| GET.Entry.Procurement														|  GET.Procurements.One.ByNumber																							   |
| GET.Entry.ProcurementBy													|  GET.Procurements.One.ById																								   |
| GET.View.ProcurementBy													|  x (GET.Procurements.One.ById)																							   |
| GET.View.ProcurementSources												|  GET.Procurements.Many.Sources																							   |
| GET.View.PopulateComponentStates											|  GET.Procurements.Many.WithComponentStates																				   |
| GET.View.ProcurementsBy(string, KindOf)									|  GET.Procurements.Many.ByKind																								   |
| GET.View.ProcurementsBy(string, bool, KindOf)								|  GET.Procurements.Many.ByDateKind																							   |
| GET.View.ProcurementsBy(bool)												|  GET.Procurements.Many.AcceptedByOverdue (Accepted)																		   |
| GET.View.ProcurementsBy(bool, KindOf)										|  GET.Procurements.Many.ByVisa																								   |
| GET.View.ProcurementsBy(string, DateTime)									|  GET.Procurements.Many.ByStateAndStartDate																				   |
| GET.View.ProcurementsNotPaid												|  GET.Procurements.Many.NotPaid (Accepted)																					   |
| GET.View.ProcurementsQueue												|  GET.Procurements.Many.CalculationQueue																					   |
| GET.View.ProcurementsManagersQueue										|  GET.Procurements.Many.ManagersQueue																						   |
| GET.View.ProcurementsBy(KindOf)											|  x(GET.Procurements.Many.ByKind)																							   |
| GET.View.ProcurementsBy(str,str,str...)											|  GET.Procurements.Many.SearchQuery																							   |
| 																			| 																														   |
| **need to test** | 													| 																														   |
| GET.View.ProcurementsBy(string, string, string, ...)						|  refactoring of this is left for a rainy day																				   |
| GET.Aggregate.GetActualProcurementI										|  GET.Procurements.One.ActualId																							   |
| GET.Aggregate.ProcurementsCountBy(string, KindOf)							|  GET.Procurements.Count.ByKind																							   |
| GET.Aggregate.ProcurementsCountBy(string, DateTime, int)					|  to proc empl																												   |
| GET.Aggregate.ProcurementsCountBy(string, DateTime)						|  GET.Procurements.Count.ByStateAndStartDate																				   |
| GET.Aggregate.ProcurementsCountBy(bool)									|  GET.Procurements.Count.Accepted																							   |
| GET.Aggregate.ProcurementsCountBy(KindOf)									|  x (GET.Procurements.Count.ByKind)																						   |
| GET.Aggregate.ProcurementsCountBy(bool, KindOf)							|  GET.Procurements.Count.ByVisa																							   |
| GET.Aggregate.ProcurementsCountBy(string, bool, KindOf)					|  GET.Procurements.Count.ByDateKind																						   |
| GET.Aggregate.ProcurementsQueueCount										|  GET.Procurements.Count.CalculationQueue																					   |
| GET.Aggregate.ProcurementsManagersQueueCount								|  GET.Procurements.Count.ManagersQueue		
| | |				
| **need to test** |														| 																														   |
| **ProcurementsEmployees** | 														| 																														   |
| PULL.ProcurementsEmployee													|  PUT.ProcurementsEmployee _dependent_: PUT.ProcurementsEmployeesBy(pe,3x string)											   |
| PUT.ProcurementsEmployeesBy(pe,3x string)									|  POST.ProcurementsEmployees.OneByPositions																				   |
| PUT.ProcurementsEmployeesBy(int)											|  POST.ProcurementsEmployees.OneByEmployee																					   |
| PUT.ProcurementsEmployees													|  POST.ProcurementsEmployees.One																							   |
| GET.View.ProcurementsEmployeesBy(string, int)								|  GET.ProcurementsEmployees.Many.ByStateAndStartDate																		   |
| GET.View.ProcurementsEmployeesBy(int, string)								|  GET.ProcurementsEmployees.Many.ByState																					   |
| GET.View.ProcurementsEmployeesBy(Procurement, string, string, string)		|  GET.ProcurementsEmployees.One.ByPositions																				   |
| GET.View.ProcurementsEmployeesBy(int) 									|  GET.ProcurementsEmployees.Many.ByEmployee																				   |
| GET.View.ProcurementsEmployeesByProcurement(int procurementId)			|  GET.ProcurementsEmployees.Many.ByProcurement																				   |																		| 																														   |
| GET.View.ProcurementsEmployeesBy(string, KindOf, int)						|   GET.ProcurementsEmployees.Many.ByKind																					   |
| GET.View.ProcurementsEmployeesBy(string, bool, KindOf, int) 				|   GET.ProcurementsEmployees.Many.ByDateKind																				   |
| GET.View.ProcurementsEmployeesBy(bool, int)								|   GET.ProcurementsEmployees.Many.Accepted																					   |
| GET.View.ProcurementsEmployeesBy(bool)										|  x (GET.ProcurementsEmployees.Many.Accepted)																				   |
| GET.View.ProcurementsEmployeesBy(KindOf, int)								|  x (GET.ProcurementsEmployees.Many.ByKind)																				   |
| GET.View.ProcurementsEmployeesBy(bool, KindOf, int)						|  GET.ProcurementsEmployees.Many.ByVisa																					   |
|																		| 																														   |
|GET.Aggregate.ProcurementsEmployeesCountBy(string, KindOf, int) 			|  GET.ProcurementsEmployees.Count.ByKind																					   |
|GET.Aggregate.ProcurementsEmployeesCountBy(KindOf, int)					|  x (GET.ProcurementsEmployees.Count.ByKind)																				   |
|GET.Aggregate.ProcurementsEmployeesCountBy(string, bool, KindOf, int)		|  GET.ProcurementsEmployees.Count.ByDateKind)																				   |
|GET.Aggregate.ProcurementsEmployeesCountBy(bool, int)						|  GET.ProcurementsEmployees.Count.Accepted)																				   |
|GET.Aggregate.ProcurementsEmployeesCountBy(int)							|  x (GET.ProcurementsEmployees.Count.Accepted)																				   |
|GET.Aggregate.ProcurementsEmployeesCountBy(bool, KindOf, int)				|  GET.ProcurementsEmployees.Count.ByVisa																					   |
|																			| 																														   |
| **----** | 																| 																														   |
|																			| 																														   |
|GET.Aggregate.ProcurementsCountBy(string, DateTime, int)					|  GET.ProcurementsEmployees.Count.ByStateAndStartDate																		   |
| | |
|GET.View.ProcurementsEmployeesGroupBy(int)									|  GET.ProcurementsEmployees.Group.ByEmployee																				   |
|GET.View.ProcurementsEmployeesGroupBy(7x string)							|  GET.ProcurementsEmployees.Group.ByPositionsAndStates																		   |
|GET.View.ProcurementsEmployeesGroupBy(3x string)							|  GET.ProcurementsEmployees.Group.ByPositions																				   |
|GET.View.ProcurementsGroupByMethod											|  GET.ProcurementsEmployees.Group.ByMethod																					   |
|																			| 																														   |
|**SupplyMonitoringLists** | 														| 																														   |
|GET.View.GetSupplyMonitoringLists											|  GET.SupplyMonitoringLists																								   |
|																			| 																														   |
|  **ProcurementDocument** | 															| 																														   |
| GET.View.ProcurementDocumentsBy											|  GET.ProcurementDocuments																									   |
|-DELETE.ProcurementDocument												|  DELETE.ProcurementDocument																								   |
| PUT.ProcurementsDocuments													|  POST.ProcurementsDocument																								   |
|																			| 																														   |
|  **ProcurementPreference** | 														| 																														   |
| GET.View.ProcurementPreferencesBy											|  GET.ProcurementPreferences																								   |
|-DELETE.ProcurementPreference												|  DELETE.ProcurementPreference																								   |
| PUT.ProcurementsPreferences												|  POST.ProcurementsPreference																								   |
|																			| 																														   |
|  **ProcurementState** | 															| 																														   |
| GET.View.ProcurementStates												|  GET.ProcurementStates.All																								   |													| 																														   |
| GET.View.DistributionOfProcurementStates									|  GET.ProcurementStates.ManyByEmployeePosition																				   |																	| 																														   |
|-DELETE.ProcurementState													|  DELETE.ProcurementState																									   |
| PUT.ProcurementState														|  POST.ProcurementState																									   |
| PULL.ProcurementState														|  PUT.ProcurementState	
|																			| 																														   |
|**Notifications** | 														| 																														   |
|GET.View.EmployeeNotificationsBy											|  GET.EmployeeNotifications																								   |
|GET.View.HasUnreadNotifications											|  GET.Notifications.HasUnread																								   |
|																			| 																														   |
|  **Others** | 	 																	| 																														   |
| PULL.ClosingActiveSessionsByEmployee										|  PUT.ClosingActiveSessionsByEmployee																						   |
| GET.View.RepresentativeTypes												|  GET.RepresentativeTypes																									   |
| GET.View.ShipmentPlans													|  GET.ShipmentPlans																										   |
| GET.View.ExecutionStates													|  GET.ExecutionStates																										   |
| GET.View.WarrantyStates													|  GET.WarrantyStates																										   |
| GET.View.SignedOriginals													|  GET.SignedOriginals																										   |
| GET.View.CommissioningWorks												|  GET.CommissioningWorks																									   |
