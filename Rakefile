# Albacore 2.x version Rakefile
# See: https://github.com/Albacore/albacore/blob/d6f9d08cd5b7a7ca97842df4032acabc83332fa2/lib/albacore/tools/fluent_migrator.rb#L17

require 'albacore/tools/fluent_migrator'

LOCAL_RUNNER = 'packages\FluentMigrator.Tools.1.3.1.0\tools\x86\40\Migrate.exe'
LOCAL_TARGET = 'Albacore-FluentMigrator-sample\bin\Debug\Albacore-FluentMigrator-sample.exe'
LOCAL_PROVIDER = 'sqlite'
LOCAL_DB = 'D:\db\albacore.db'
SQLITE_CONNECTION = "\"Data Source=#{LOCAL_DB}\""

namespace :db do
  desc "migrator task"     
  task :migrator, :task do |migrator, args|
    # these could also be defined in a YML file
    raise "ERROR: :task must be defined" if args[:task].nil?

    ns = Albacore::Tools::FluentMigrator
    cmd = ns::MigrateCmdFactory.create exe: "#{LOCAL_RUNNER}", 
                                       dll: "#{LOCAL_TARGET}", 
                                       db: "#{LOCAL_PROVIDER}", 
                                       conn: "#{SQLITE_CONNECTION}",
                                       direction: args[:task],
                                       extras: "--steps=2",  # 単一
                                       # extras: ["--steps=2", "--output=true"], # 複数
                                       interactive: false  # trueでは逐一入力が必要
    cmd.execute
  end

  namespace :rollback do
    desc "Rollback the database one iteration"
    task :default do |migrator|
      Rake::Task["db:migrator"].reenable
      Rake.application.invoke_task("db:migrator[rollback]")
    end
  end

  task :rollback => "rollback:default"


  namespace :migrate do
    desc "migrate to current version"      
    task :up do |migratecmd|   
      Rake::Task["db:migrator"].reenable

      # 1.ダブルクオーテーションで囲む場合
      # -conn "Data Source=D:\db\albacore.db" --timeout=200 --task "migrate"
      # => 1: InitialMigration migrating が実行されない
      # 2.ダブルクオーテーションで囲まない場合
      #　-conn "Data Source=D:\db\albacore.db" --timeout=200 --task migrate 
      # => #1: InitialMigration migrating が実行される
      Rake.application.invoke_task("db:migrator[migrate]")  
    end

    desc "migrate down"
      task :down do |migratecmd|  
      Rake::Task["db:migrator"].reenable
      Rake.application.invoke_task("db:migrator[migrate:down]")
    end

    desc "Redo the last migration"
      task :redo => ["db:rollback", "db:migrate"] do |task|
      puts "Redo complete"
    end
  end

  task :migrate => "migrate:up"
end