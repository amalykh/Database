   M S B A M L   `   `   `  ����   DataBaseProject&     DataBaseProject.Views.TablesView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .���   � RowDefinitions  �� $
��363* ��� $	��28* � � ColumnDefinitions �� $
��155* ��� $	��53* ��� $
��437* �.+�� ���	#FFE5E5E5$��3��$��2��  �� $	��64* ��� $
��279* ��� $	��21* ��� $��5* ��� $	��22* � �� $
��142* ��� $
��503* �.+��� -   $��TablesListDataGrid�� �� SelectionMode$ Single=�$��2�� �� SelectedIndex �� *SelectedTableIndex+���� *Tables+�� $��1��$��5��.@��� $��Table��.��� ���	#FFE5E5E5 �� $��9* ��� $	��69* ��� $��5* ��� $	��69* ��� $��8* ��� $	��66* ��� $
��237* �  �� $	��12* ��� $	��22* ��� $��7* ��� $
��350* �.+��� -   $��InsertColumn��$��Insert column��$��1��$��1���� -   $��DeleteColumn��$��Delete column��$��1��$��3���� -   $��
EditColumn��$��Edit column��$��5��$��1��&   System.Windows.Controls.DataGrid     
IsReadOnly .    CanUserSortColumns .     CanUserReorderColumns .  $��3��$��7�� �� *SelectedTableColumnIndex+���� *	Columns+   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  	 �� BasedOn#	 [B .��� 
 �� Property
 � �� �� Value �� .R��� #��z��$��Center=��� $
��Data��.��� ���	#FFE5E5E5 �� $��9* ��� $	��69* ��� $��5* ��� $	��69* ��� $��8* ��� $	��66* ��� $
��237* �  �� $	��12* ��� $	��22* ��� $��7* ��� $
��350* �.+��� -   $��	InsertRow��$��
Insert row��$��1��$��1���� -   $��	DeleteRow��$��
Delete row��$��1��$��3���� -   $��EditRow��$��Edit row��$��5��$��1��  -   $��DataTableDataGrid��$��3��$��1��$��6�� .    SelectionMode$ Single=� .   .  ���� *TableDataTable+ �� *SelectedDataTableIndex+ �� "  #	 [B .��� 
 � �� �� .R��� #��z��$��Center=��� -	   $��AddTable��$��	Add table��$��2���� -
   $��	DropTable��$��
Drop table��$��4��