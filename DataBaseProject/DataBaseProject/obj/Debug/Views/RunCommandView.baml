   M S B A M L   `   `   `  ����  DataBaseProject*    $DataBaseProject.Views.RunCommandView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � ColumnDefinitions  5	   
   �� 5
      $	��42* �6   �� 5      $��5* �6    � RowDefinitions 5   
   �� 5      $	��10* �6   �� 5      $��3* �6   �� 5      $
��148* �6   �� 5      $
��139* �6   .+�5   
   �� -   5         ��Name$	 Run��5      $	��Run��6   $��0��6,   $��1��69   �� 5   
   -   5         ��Name$ Command��5      $�� ��6#   $��2��6+   $��2��68    �� TextWrapping$
 Wrap=�6L    �� AcceptsReturn . $��14(�6u   � 5   
   ���	#FFE5E5E5$��3��6&   $��2��63   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��RunResultDataGrid��5      ��60   �� *QueryResultReader+   ColumnHeaderStyle 5      �� 5       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    �� BasedOn# [B 6G   .�5      �� 	 �� Property	 � ��
 �� Value
 5      �� 5   "   .R�5   &   �� #��z��60   $��Center=�6Q   5    #   5"      5#      5%      5&      5(      5)      